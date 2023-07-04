using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puertas.Variables;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.Timeline;

public class SnakeController : MonoBehaviour
{

    [SerializeField] FloatReference playerY; //este esta al pedo ya que tengo todo abajo
    Transform player;
    [SerializeField] GameObject hitbox;

    [SerializeField] AudioClip attackSound;

    [SerializeField] float atkCD = 0.5f;
    [SerializeField] float range = 2f;
    [SerializeField] float detectionRange = 7f;

    Rigidbody2D rb;
    Animator anim;
    Vector3 initialPosition;

    public Vector3 attackOffset;
    public float attackRange = 1f;


    bool canMove = true;
    bool canAttack = true;
    bool isFlipped = false;





    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckZAxis();
        CheckMovePosition();

    }


    void CheckZAxis()
    {
        int value;
        if (playerY.Value > transform.position.y - 0.20f)
        {
            value = 2;
        }
        else value = 0;

        GetComponent<SpriteRenderer>().sortingOrder = value;
    }

    void CheckMovePosition()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, initialPosition, 5f * Time.fixedDeltaTime);

        float playerToEnemyDistance = Vector3.Distance(player.position, transform.position);
        if (playerToEnemyDistance < detectionRange)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
            rb.MovePosition(newPosition);
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        if (isFlipped)
        {
            pos += new Vector3(1,0,0);
        }
        else pos += new Vector3(-1, 0, 0);

        SoundManager.Instance.PlaySound(attackSound);
        GameObject go = Instantiate(hitbox, pos, Quaternion.identity);
        Destroy(go, 0.2f);
    }

    public void getDamage()
    {
        
        StartCoroutine(StopMovement());
    }

    IEnumerator StopMovement()
    {
        canAttack = false;
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
        canMove = true;

    }



    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
