using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Puertas.Variables;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject CharacterText;
    [SerializeField] GameObject UiHpText;
    [SerializeField] float textTimer = 2f;
    public static TextManager Instance { get; private set; }


    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ChangeHp(int value)
    {
        UiHpText.GetComponent<Text>().text = $"Player HP: {value}";
    }

    public void ShowTextOverCharacter(string _text)
    {
        StartCoroutine(ShowText(_text));
    }

    private IEnumerator ShowText(string _text)
    {
        CharacterText.GetComponent<Text>().text = _text;
        CharacterText.SetActive(true);
        yield return new WaitForSeconds(textTimer);
        CharacterText.SetActive(false);

    }
}
