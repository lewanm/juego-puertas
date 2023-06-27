using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    private void Update()
    {
        transform.Translate(new Vector3(0.001f, 0, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.GetComponent<MonsterHP>().TakeDamage();
        }
    }

}
