using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextosTuki : MonoBehaviour
{
    [SerializeField, TextArea(2,3)] string[] textos;
    [SerializeField] GameObject player;
    [SerializeField] GameObject END;
    Text text;
    Animator anim;
    int index;

    GameObject destroyOnLoad;
    

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        index = 0;
        anim = GetComponent<Animator>();
        destroyOnLoad = GameObject.FindGameObjectWithTag("testing");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Destroy(destroyOnLoad);
        }
    }

    public void ChangeNextText()
    {
        text.text = textos[index];
        if(index < textos.Length - 1)
        {
            index++;
        }
    }

    public void HideText()
    {
        text.color = new Color(1, 1, 1, 0);
    }

    public void ShowText()
    {
        text.color = new Color(1, 1, 1, 1);
    }
    public void SetNextPlayerAnimation()
    {
        player.GetComponent<PlayerAnimation>().StartAnimation();
    }

    public void StartAnimation()
    {
        anim.SetTrigger("SegundoTexto");
    }

    public void EndGame()
    {
        END.SetActive(true);
    }

    public void TrueHide()
    {
        text.gameObject.SetActive(false);
    }

}
