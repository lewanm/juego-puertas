using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitboxes : MonoBehaviour
{
    bool state = false;
    Text buttonText;
    private void Start()
    {
        buttonText =  transform.GetChild(0).GetComponent<Text>();
        buttonText.text = state ? "ON" : "OFF";
    }
    public void OnPress()
    {
        state = !state;
        buttonText.text = state ? "ON" : "OFF";
        ManagerGame.Instance.ShowAllHitboxes(state);
    }
}
