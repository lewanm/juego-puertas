using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    [SerializeField] string itemName;
    public void Action(GameObject player)
    {
        if(itemName == "Key") player.GetComponent<PlayerController>().GetKey();
        if(itemName == "Weapon") player.GetComponent<PlayerController>().GetWeapon();
        Destroy(gameObject);
    }

}
