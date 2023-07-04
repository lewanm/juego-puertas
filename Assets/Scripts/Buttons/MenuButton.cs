using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] Sprite imagen1, imagen2;
    [SerializeField] int sceneToLoad;
    [SerializeField] AudioClip hoverSound, clickSound;

    GameObject destroyOnLoad;
    // Start is called before the first frame update
    void Start()
    {
        destroyOnLoad = GameObject.FindGameObjectWithTag("testing");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mouseOn()
    {
        GetComponent<Image>().sprite = imagen2;
        if(hoverSound != null) SoundManager.Instance.PlaySound(hoverSound);
    }

    public void mouseOff()
    {
        GetComponent<Image>().sprite = imagen1;
    }

    public void ChangeEscene()
    {
        if(clickSound!= null) SoundManager.Instance.PlaySound(clickSound);
        if(destroyOnLoad != null) Destroy(destroyOnLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
