using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puertas.Variables;

public class ChestController : MonoBehaviour
{
    //[SerializeField] GameObject item;
    [SerializeField] Sprite chestHovered;
    [SerializeField] AudioClip openChestSound;
    [SerializeField] ItemSO item;
    bool opened = false;

    public void Action(GameObject player)
    {
        if (opened) return;

        if (openChestSound != null) SoundManager.Instance.PlaySound(openChestSound);
        opened = true;
        if(chestHovered != null) GetComponent<SpriteRenderer>().sprite = chestHovered;
        player.GetComponent<PlayerController>().GetItem(item);
        //StartCoroutine(ActionAnim(player));
        
    }

    IEnumerator ActionAnim(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        
    }
}
