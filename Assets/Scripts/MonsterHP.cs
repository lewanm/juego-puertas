using Puertas.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    [SerializeField] IntReference initialHp;
    [SerializeField] int hp;
    [SerializeField] GameObject DropItem;
    private void Start()
    {
        hp = initialHp.Value;
    }

    private void Update()
    {
        CheckHP();

    }
    public void TakeDamage()
    {
        hp -= 1;
    }

    void CheckHP()
    {
        if (hp <= 0)
        {
            Instantiate(DropItem,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
