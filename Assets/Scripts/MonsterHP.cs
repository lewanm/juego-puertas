using Puertas.Variables;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    [SerializeField] IntReference initialHp;
    [SerializeField] int hp;
    [SerializeField] GameObject DropItem;

    private Animator animator;
    private void Start()
    {
        hp = initialHp.Value;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckHP();

    }
    public void TakeDamage()
    {
        StartCoroutine(TakeDamageCoroutine());
    }

    IEnumerator TakeDamageCoroutine()
    {
        
        animator.SetTrigger("Damaged");

        switch (gameObject.name)
        {
            case "Sname":
                GetComponent<SnakeController>().getDamage();
                break;
        }

        yield return new WaitForSeconds(0.5f);
        hp--;
    }

    void CheckHP()
    {
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if(DropItem != null) Instantiate(DropItem, transform.position, Quaternion.identity);

        ManagerGame.Instance.SumKillCount();
        Destroy(gameObject);
    }




}
