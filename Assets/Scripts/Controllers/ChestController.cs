using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] Sprite chestHovered;
    bool opened = false;

    public void Action(GameObject player)
    {
        if(!opened)
        {
            player.GetComponent<PlayerController>().GetItem(item);
            opened = true;
            GetComponent<SpriteRenderer>().sprite = chestHovered;
        }
    }
}
