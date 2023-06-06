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

    [SerializeField] int initialHP;
    [SerializeField] IntReference playerHP;

    Rigidbody2D rb;
    Vector2 lastDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHP.Value = initialHP;
        TextManager.Instance.ChangeHp(playerHP.Value);
    }


    private void Update()
    {
        movePlayer();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.C)) TakeDamage();
    }

    void movePlayer()
    {
        /*Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x = -1;
            animator.SetInteger("Direction", 3);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            dir.x = 1;
            animator.SetInteger("Direction", 2);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir.y = 1;
            animator.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            dir.y = -1;
            animator.SetInteger("Direction", 0);
        }

        lastDir = dir;
        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = speed * dir;*/

        rb.velocity = Vector2.zero;

        Vector2 direction = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();

        rb.velocity = speed * direction;
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
            Debug.Log(lastDir);
        }
        else
        {
            TextManager.Instance.ShowTextOverCharacter("No tengo un arma aún");
        }
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

