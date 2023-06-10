using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Puertas.Variables;


[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool hasWeapon = false;
    [SerializeField] bool hasKey = false;
    [SerializeField] Sprite[] sprites;
    Animator animator;

    [SerializeField] int initialHP;
    [SerializeField] IntReference playerHP;

    [SerializeField] float atkCD = 0.5f; 
    [SerializeField] bool atkOnCD = false;

    private void Start()
    {

        playerHP.Value = initialHP;
        TextManager.Instance.ChangeHp(playerHP.Value);
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction", 2);
    }


    private void Update()
    {
        if(!atkOnCD) movePlayer();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.C)) TakeDamage();
    }

    void movePlayer()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir.y = 1;
            animator.SetInteger("Direction", 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            dir.x = 1;
            animator.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            dir.y = -1;
            animator.SetInteger("Direction", 2);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x = -1;
            animator.SetInteger("Direction", 3);
        }


        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
        dir.Normalize();
        animator.SetBool("isMoving", dir.magnitude > 0);
        

        GetComponent<Rigidbody2D>().velocity = speed * dir;

        /*lastDir = dir;
        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = speed * dir;

        rb.velocity = Vector2.zero;

        Vector2 direction = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();

        rb.velocity = speed * direction;*/
    }

    //mover esto despues al gameManager
    void TakeDamage()
    {
        playerHP.Value -= 1;
        
        TextManager.Instance.ChangeHp(playerHP.Value);
    }
    void Attack()
    {
        if (hasWeapon)
        {
            if(!atkOnCD) StartCoroutine(C_Attack());
        }
        else
        {
            TextManager.Instance.ShowTextOverCharacter("No tengo un arma aún");
        }
    }

    IEnumerator C_Attack()
    {
        atkOnCD = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.SetTrigger("isAttacking");
        yield return new WaitForSeconds(atkCD);
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
    }

    public void GetKey()
    {
        hasKey = true;
        //Ver si crear otro texto arriba en la UI en vez de arriba del player.
        TextManager.Instance.ShowTextOverCharacter("Obtuve una llave...");
    }
    public bool HasKey()
    {
        return hasKey;
    }

    public void GetWeapon()
    {
        hasWeapon = true;
    }
}

