using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Puertas.Variables;
using UnityEditor;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool hasWeapon = false;
    [SerializeField] bool hasKey = false;
    [SerializeField] bool isDark = false;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject swordHitbox;

    [SerializeField] int initialHP;
    [SerializeField] IntReference playerHP;

    [SerializeField] float atkCD = 0.55f;
    [SerializeField] bool atkOnCD = false;

    [SerializeField] AudioClip attackSound;
    Vector3 swordTarget;
    Animator animator;

    private void Start()
    {
        playerHP.Value = initialHP;
        TextManager.Instance.ChangeHp(playerHP.Value);
        animator = GetComponent<Animator>();
        animator.SetFloat("Direction", 2f);
        swordTarget = transform.position + new Vector3(0f, -0.8f, 0);
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

        //dev only
        if (Input.GetKeyDown(KeyCode.C)) TakeDamage();
    }
    void CheckLight()
    {
        if (!isDark) animator.SetFloat("isDark", 0f);
        else animator.SetFloat("isDark", 1f);

    }
    void CheckPosition()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Ceil(Mathf.Abs(transform.position.y - 0.4f) * 10);
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

    //mover esto despues al gameManager
    void TakeDamage()
    {
        playerHP.Value -= 1;

        TextManager.Instance.ChangeHp(playerHP.Value);
    }
    void Attack()
    {
        if (hasWeapon && !isDark)
        {
            if (!atkOnCD) StartCoroutine(C_Attack());
        }
        
        if (!hasWeapon)
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
        //go.transform.Translate(new Vector3(0.001f, 0, 0));
        yield return new WaitForSeconds(atkCD);
        Destroy(go);
        atkOnCD = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.X) && collision.gameObject.CompareTag("Door"))
        {
            collision.GetComponent<DoorController>().Action(gameObject);
        }

        if (Input.GetKey(KeyCode.X) && collision.gameObject.CompareTag("Item"))
        {
            collision.GetComponent<ItemsController>().Action(gameObject);
        }

        if (collision.gameObject.CompareTag("Testing"))
        {
            TextManager.Instance.ChangeFinalText("El prototipo termina hasta este punto, y esta son algunas cosas que vamos a agregar. Monstruos con sus respectivos ataques, y un jefe final. ");
        }
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