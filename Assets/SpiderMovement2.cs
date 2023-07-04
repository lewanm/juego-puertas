using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpiderMovement2 : MonoBehaviour
{

    [SerializeField] float speed = 3f;
    [SerializeField] Transform spawner1, spawner2;
    [SerializeField] GameObject snake;
    bool canMove = true;

    float timer, spawnTimer;
    int randomNumber, randomSpanwNumber;

    Transform player;
    Rigidbody2D rb;
    Animator spider;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spider = gameObject.transform.GetChild(0).GetComponent<Animator>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayerX();

        timer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        randomNumber = Random.Range(4, 7);
        randomSpanwNumber = Random.Range(7, 12);

        if (timer > randomNumber) Attack();
        if (spawnTimer > randomSpanwNumber) SpawnSnakes();

        /*if(Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(StartFirstAttack());
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(StartSecondtAttack());
        }*/
    }

    void Attack()
    {
        randomNumber = Random.Range(2, 5);

        timer = -2;
        if(Random.Range(0,10) > 7) StartCoroutine(StartFirstAttack());
        else StartCoroutine(StartSecondtAttack());
    }

    void SpawnSnakes()
    {
        Vector3 pos;
        spawnTimer = -4;
        randomSpanwNumber = Random.Range(7, 12);
        if (Random.Range(0, 10) >= 5)
        {
            pos = spawner1.position;
        }
        else 
        {
            pos = spawner2.position;
        }
        Instantiate(snake, pos, Quaternion.identity);

    }

    IEnumerator StartFirstAttack()
    {
        spider.SetTrigger("Attack1");
        canMove = false;
        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }

    IEnumerator StartSecondtAttack()
    {
        spider.SetTrigger("Attack2");
        gameObject.layer = 9;
        yield return new WaitForSeconds(1.5f);
        gameObject.layer = 0;

    }

    void FollowPlayerX()
    {
        if (!canMove) return;

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }


}
