using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SpiderController : MonoBehaviour
{
    float timer;
    Animator animator;
    [SerializeField] GameObject hitbox;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void Attack()
    {
        if (timer > RandomSecondsBetween(3,6))
        {
            Attack1();
            timer = -5;
            /*
            if (Random.Range(0, 1) == 1)
            {
                animator.SetTrigger("Attack1");
            }
            else
            {
                Attack2();
            }
            */
        }
    }

    float RandomSecondsBetween(int n1, int n2)
    {
        return Random.Range(n1, n2);
    }

    //Hacer que no se pueda mover fuera de los bordes
    public void Attack1()
    {
        Vector2 targetPost = transform.position + Vector3.down * 2.2f;
        GameObject go = Instantiate(hitbox, targetPost, Quaternion.identity);
        Destroy(go, 0.35f);
    }

    public void Attack2()
    {

    }
}
