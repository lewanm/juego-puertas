using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour
{
    [SerializeField] Sprite imagen1;
    [SerializeField] Sprite imagen2;
    // Start is called before the first frame update
    void Start()
    {
        
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

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
