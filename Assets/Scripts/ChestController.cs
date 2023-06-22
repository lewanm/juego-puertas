using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] Sprite chestHovered;
    bool hasItem = true;

    public void Action(GameObject player)
    {
        if(hasItem)
        {
            player.GetComponent<PlayerController>().GetItem(item);
            hasItem = false;
            GetComponent<SpriteRenderer>().sprite = chestHovered;
            
        }

        
    }
}
