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
{

    [Header("Player Parameters")]
    [SerializeField] float atkCD = 0.55f;
    [SerializeField, Min(3)] int initialHP;
    [SerializeField] IntReference playerHP;
    [SerializeField] FloatReference playerY;
    [SerializeField, Range(2,6)] float speed;

    [Header("Dev")] //Variables privadas pero que se muestran en el inspector para mas facil acceso.
    [SerializeField] bool hasWeapon = false;
    [SerializeField] bool hasKey = false;
    [SerializeField] bool isDark = false;
    [SerializeField] bool hasTorch = false;
    [SerializeField] bool startFromStartPosition = false;
    [SerializeField] GameObject swordHitbox;

    [Header("Sounds")]
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip playerDamaged;
    [SerializeField] AudioClip walkSound;



    bool atkOnCD = false;
    Transform mascara;
    Vector3 startPosition;
    Vector3 swordTarget;
    Animator animator;
    bool invincible = false;

    private void Start()
    {
        playerHP.Value = initialHP;
        //TextManager.Instance.ChangeHp(playerHP.Value);
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
        CheckPosition();
        if (!atkOnCD) MovePlayer();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartAttackAnimation();
            
        }
        CheckLight();
    }

    void StartAttackAnimation()
    {
        if (hasWeapon && !isDark)
        {
            if (!atkOnCD)
            {
                animator.SetTrigger("Attack");
                SoundManager.Instance.PlaySound(attackSound);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StartCoroutine(SetAttackOnCD(0.5f));

            }
        }

        if (!hasWeapon && !isDark)
        {
            TextManager.Instance.ShowTextOverCharacter("No tengo un arma aún");
        }
    }

    public void Attack()
    {
        GameObject go = Instantiate(swordHitbox, swordTarget, Quaternion.identity);
        Destroy(go, 0.3f);
    }

    IEnumerator SetAttackOnCD(float atkCD)
    {
        atkOnCD = true;
        yield return new WaitForSeconds(atkCD);
        atkOnCD = false;
    }

    void CheckLight()
    {

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
            mask.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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

    void MovePlayer()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir.y = 1;
            animator.SetFloat("Direction", 0);
            swordTarget = transform.position + new Vector3(0, 1f, 0);
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
            swordTarget = transform.position + new Vector3(0f, -0.7f, 0);
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
        //SoundManager.Instance.PlaySound(walkSound);

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }

    bool PressedActionButton()
    {
        return Input.GetKey(KeyCode.X);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //AYUDA COMO HAGO ESTO CON POO, A SI LE TODOS HEREDAN DE UNA MISMA CLASE Y NO TENER QUE HACER ESTA NEGRADA
        if (PressedActionButton() && collision.gameObject.CompareTag("Door"))
        {
            collision.GetComponent<DoorController>().Action(gameObject);
        }

        if (PressedActionButton() && collision.gameObject.CompareTag("spiderweb"))
        {
            collision.GetComponent<SpiderwebController>().Burn(gameObject);
        }

        if (PressedActionButton() && collision.gameObject.CompareTag("Chest"))
        {
            collision.GetComponent<ChestController>().Action(gameObject);
        }

        if (PressedActionButton() && collision.gameObject.CompareTag("Item"))
        {
            collision.GetComponent<ItemsController>().Action(gameObject);
        }

        if (collision.gameObject.CompareTag("ExitDoor"))
        {
            collision.GetComponent<DoorController>().Action(gameObject);
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
            //TextManager.Instance.ChangeHp(playerHP.Value);
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
        SoundManager.Instance.PlaySound(playerDamaged);
        yield return new WaitForSeconds(0.6f);
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
    public bool HasTorch()
    {
        return hasTorch;
    }



    public void GetItem(ItemSO item)
    {
        //string itemName = item.GetComponent<ItemsController>().itemName;
        //AudioClip itemSound  = item.GetComponent<AudioClip>();
        string itemName = item.itemName;
        AudioClip itemSound = item.itemSound;
        GameObject itemPrefab = item.itemPrefab;


        if (itemName == "Sword")
        {
            hasWeapon = true;
        }

        if (itemName == "Key")
        {
            hasKey = true;
        }

        if (itemName == "Torch")
        {
            hasTorch = true;
        }

        if (itemName == "Potion")
        {
            playerHP.Value = initialHP;
        }
        StartCoroutine(AnimationGetItem(itemPrefab, itemSound));
    }

    IEnumerator AnimationGetItem(GameObject item, AudioClip itemSound)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        atkOnCD = true;

        animator.SetTrigger("GetItem");
        GameObject go = Instantiate(item, transform.position + Vector3.up, Quaternion.identity);
        SoundManager.Instance.PlaySound(itemSound);

        yield return new WaitForSeconds(0.5f);

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