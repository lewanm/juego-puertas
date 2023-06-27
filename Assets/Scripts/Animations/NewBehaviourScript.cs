using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject miniRoman;
    [SerializeField] GameObject text;
    bool estaEnPosicion = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AbrirPergamino());
    }

    // Update is called once per frame
    void Update()
    {
        if (estaEnPosicion && Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
    }

    IEnumerator AbrirPergamino()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        StartCoroutine(MiniRoman());
    }

    IEnumerator MiniRoman()
    {
        yield return new WaitForSeconds(2f);
        miniRoman.SetActive(true);
        text.SetActive(true);
        estaEnPosicion = true;
    }


}
