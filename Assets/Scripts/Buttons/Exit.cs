using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{

    [SerializeField] Sprite imagen1;
    [SerializeField] Sprite imagen2;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Image>().color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mouseOn()
    {
        GetComponent<Image>().sprite = imagen2;
    }

    public void mouseOff()
    {
        GetComponent<Image>().sprite = imagen1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
