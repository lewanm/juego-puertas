using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] Sprite door, doorHovered, doorActived;
    [SerializeField] GameObject positionTarget;
    [SerializeField] Transform cameraPositionTarget;
    [SerializeField] bool doorIsLocked = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canTheDoorBeOpened(collision.gameObject))
        {
            GetComponent<SpriteRenderer>().sprite = doorHovered;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sprite = door;
        }
    }

    bool canTheDoorBeOpened(GameObject player)
    {
        return (!doorIsLocked || doorIsLocked && player.GetComponent<PlayerController>().HasKey());
    }

    void OpenDoor(GameObject player)
    {
        GetComponent<SpriteRenderer>().sprite = doorActived;
        player.transform.position = positionTarget.transform.position;
        Camera.main.transform.position = cameraPositionTarget.position;
    }

    public void Action(GameObject player)
    {
        if (canTheDoorBeOpened(player)) OpenDoor(player);
        else TextManager.Instance.ShowTextOverCharacter("La puerta esta cerrada");
    }


}
