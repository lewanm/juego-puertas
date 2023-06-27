using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{

    public string itemName;
    [SerializeField] AudioClip pickSwordSound, pickItemSound;
    public void Action(GameObject player)
    {
        if(itemName == "Key")
        {
            player.GetComponent<PlayerController>().GetKey();
            SoundManager.Instance.PlaySound(pickItemSound);
        }
        if(itemName == "Weapon")
        {
            player.GetComponent<PlayerController>().GetWeapon();
            SoundManager.Instance.PlaySound(pickSwordSound);
        }
        Destroy(gameObject);
    }

}
