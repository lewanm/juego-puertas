using Puertas.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] Sprite door, doorHovered, doorActived;
    [SerializeField] GameObject positionTarget;
    [SerializeField] Transform cameraPositionTarget;
    [SerializeField] bool doorIsLocked = false;
    [SerializeField] AudioClip openDoor;
    [SerializeField] int killedEnemiesToBeOpen;
    [SerializeField] IntReference killCount;
    [SerializeField] bool nextRoomIsDark;
    [SerializeField] bool nextRoomHaveTraps;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canTheDoorBeOpen(collision.gameObject))
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

    bool canTheDoorBeOpen(GameObject player)
    {
        return (
            !doorIsLocked || 
            (doorIsLocked && player.GetComponent<PlayerController>().HasKey())) && killedEnemiesToBeOpen == 0 || 
            (doorIsLocked && killCount.Value >= killedEnemiesToBeOpen && killedEnemiesToBeOpen != 0);
    }

    void OpenDoor(GameObject player)
    {
        player.GetComponent<PlayerController>().LightTrigger(nextRoomIsDark);

        player.transform.position = positionTarget.transform.position;
        Camera.main.transform.position = cameraPositionTarget.position;

        GetComponent<SpriteRenderer>().sprite = doorActived;
        SoundManager.Instance.PlaySound(openDoor);

        if (nextRoomHaveTraps) ManagerGame.Instance.TurnOnSpikes();
        else ManagerGame.Instance.TurnOffSpikes();

    }

    public void Action(GameObject player)
    {
        if (canTheDoorBeOpen(player)) OpenDoor(player);
        if (killCount.Value < killedEnemiesToBeOpen && killedEnemiesToBeOpen != 0)
        {
            TextManager.Instance.ShowTextOverCharacter("Hay enemigos cerca");
            return;
        }
        if (doorIsLocked && !player.GetComponent<PlayerController>().HasKey() && killedEnemiesToBeOpen == 0)
        {
            TextManager.Instance.ShowTextOverCharacter("La puerta esta cerrada");
        }

        
    }


}
