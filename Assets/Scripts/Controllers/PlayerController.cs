using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Puertas.Variables;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;


[SelectionBase]
public class PlayerController : MonoBehaviour
{//dasdas
    [SerializeField] float speed;
    [SerializeField] bool hasWeapon = false;
    [SerializeField] bool hasKey = false;
    [SerializeField] bool isDark = false;
    [SerializeField] bool hasTorch = false;
    [SerializeField] bool startFromStartPosition = false;
    [SerializeField] GameObject swordHitbox;

    Vector3 startPosition;
    [SerializeField] int initialHP;
    [SerializeField] IntReference playerHP;
    [SerializeField] FloatReference playerY;

    [SerializeField] float atkCD = 0.55f;
    [SerializeField] bool atkOnCD = false;

    Transform mascara;

    [SerializeField] AudioClip attackSound;
    Vector3 swordTarget;
    Animator animator;

    public bool invincible = false;

    private void Start()
    {
        playerHP.Value = initialHP;
        TextManager.Instance.ChangeHp(playerHP.Value);
        animator = GetComponent<Animator>();
        animator.SetFloat("Direction", 2f);
        swordTarget = transform.position + new Vector3(0f, -0.8f, 0);
        mascara = transform.GetChild(0);
        startPosition = new Vector3(0,-3,0);

        if (startFromStartPosition) transform.position = startPosition;

        if (Input.GetKey(KeyCode.C))
        {
            hasTorch = !hasTorch;
        }

    }


    private void Update()
    {

        //este metodo es para cambiar el Z y que quede atras del enemigo
        CheckPosition();
        if (!atkOnCD) movePlayer();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

        CheckLight();

    }
    void CheckLight()
    {
        //if (!isDark && hasTorch) animator.SetFloat("isDark", 0f);
        //else animator.SetFloat("isDark", 1f);
        GameObject mask = mascara.gameObject;
        if (isDark)
        {
            mask.SetActive(true); 
            if (hasTorch)
            {
                mask.transform.localScale = new Vector3(1f, 1f, 1f) ;
                animator.SetFloat("isDark", 1f);
            }
        }
        else
        {
            mask.SetActive(false);
            animator.SetFloat("isDark", 0f);
            mask.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public void LightTrigger(bool _isDark)
    {
        isDark = _isDark;
    }

    void CheckPosition()
    {

        playerY.Value = transform.position.y;

    }

    void movePlayer()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir.y = 1;
            animator.SetFloat("Direction", 0);
            swordTarget = transform.position + new Vector3(0, 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir.x = 1;
            animator.SetFloat("Direction", 1);
            swordTarget = transform.position + new Vector3(0.6f, -0.5f, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dir.y = -1;
            animator.SetFloat("Direction", 2);
            swordTarget = transform.position + new Vector3(0f, -0.8f, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x = -1;
            animator.SetFloat("Direction", 3);
            swordTarget = transform.position + new Vector3(-0.7f, -0.4f, 0);
        }

        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
        dir.Normalize();
        animator.SetBool("isMoving", dir.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }



    void Attack()
    {
        if (hasWeapon && !isDark)
        {
            if (!atkOnCD) StartCoroutine(C_Attack());
        }
        
        if (!hasWeapon && !isDark)
        {
            TextManager.Instance.ShowTextOverCharacter("No tengo un arma aún");
        }
    }

    IEnumerator C_Attack()
    {
        atkOnCD = true;
        SoundManager.Instance.PlaySound(attackSound);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.SetTrigger("isAttacking");
        GameObject go = Instantiate(swordHitbox, swordTarget, Quaternion.identity);
        yield return new WaitForSeconds(atkCD);
        Destroy(go);
        atkOnCD = false;
    }

    bool PressedActionButton()
    {
        return Input.GetKey(KeyCode.X);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PressedActionButton() && collision.gameObject.CompareTag("Door"))
        {
            collision.GetComponent<DoorController>().Action(gameObject);
        }

        if (PressedActionButton() && collision.gameObject.CompareTag("spiderweb"))
        {
            collision.GetComponent<SpiderwebController>().Burn();
        }

        if (collision.gameObject.CompareTag("ExitDoor"))
        {
            collision.GetComponent<DoorController>().Action(gameObject);
        }

        if (PressedActionButton() && collision.gameObject.CompareTag("Chest"))
        {
            collision.GetComponent<ChestController>().Action(gameObject);
        }

        if (PressedActionButton() && collision.gameObject.CompareTag("Item"))
        {
            collision.GetComponent<ItemsController>().Action(gameObject);
        }

        if (collision.gameObject.CompareTag("Testing"))
        {
            TextManager.Instance.ChangeFinalText("El prototipo termina hasta este punto, y esta son algunas cosas que vamos a agregar. Monstruos con sus respectivos ataques, y un jefe final. ");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (invincible) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHP.Value -= 1;
            TextManager.Instance.ChangeHp(playerHP.Value);
            StartCoroutine(BecomeTemporarilyInvincible());

            if (playerHP.Value <= 0)
            {
                SceneManager.LoadScene(4);
            }
        }
        
    }

    IEnumerator BecomeTemporarilyInvincible()
    {
        
        invincible = true;
        animator.SetTrigger("Damaged");
        yield return new WaitForSeconds(1);
        invincible = false;
    }

    public void GetKey()
    {
        hasKey = true;
        //Ver si crear otro texto arriba en la UI en vez de arriba del player.
        TextManager.Instance.ShowTextOverCharacter("Obtuve una llave...");
    }

    public void DropKey()
    {
        hasKey = true;
    }
    

    public bool HasKey()
    {
        return hasKey;
    }

    public void GetItem(GameObject item)
    {
        if (item.GetComponent<ItemsController>().itemName == "Weapon")
        {
            hasWeapon = true;
        }

        if (item.GetComponent<ItemsController>().itemName == "Key")
        {
            hasKey = true;
        }
        StartCoroutine(AnimationGetItem(item));
    }

    IEnumerator AnimationGetItem(GameObject item)
    {
        atkOnCD = true;
        SoundManager.Instance.PlaySound(attackSound);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.SetTrigger("GetItem");
        GameObject go = Instantiate(item, transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(atkCD); // este mismo tiempo tiene que tener la transicion de la animacion getItem
        Destroy(go);

        atkOnCD = false;
    }

    public void GetWeapon()
    {
        hasWeapon = true;
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(2, 1.3f, 0);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(swordTarget, size);
    }
}