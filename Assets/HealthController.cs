using Puertas.Variables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    Transform[] hearts;

    [SerializeField] IntReference playerHp;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] Sprite halfHeart;

    void Start()
    {
        hearts = GetComponentsInChildren<Transform>().Skip(1).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHpUi();
    }

    void UpdateHpUi()
    {
        //OJALA NADIE VEA ESTO PLS

        if (playerHp.Value == 1) {
            hearts[0].GetComponent<Image>().sprite = halfHeart;
            hearts[1].GetComponent<Image>().sprite = emptyHeart;
            hearts[2].GetComponent<Image>().sprite = emptyHeart;
        }

        if (playerHp.Value == 2)
        {
            hearts[0].GetComponent<Image>().sprite = fullHeart;
            hearts[1].GetComponent<Image>().sprite = emptyHeart;
            hearts[2].GetComponent<Image>().sprite = emptyHeart;
        }

        if (playerHp.Value == 3)
        {
            hearts[0].GetComponent<Image>().sprite = fullHeart;
            hearts[1].GetComponent<Image>().sprite = halfHeart;
            hearts[2].GetComponent<Image>().sprite = emptyHeart;
        }

        if (playerHp.Value == 4)
        {
            hearts[0].GetComponent<Image>().sprite = fullHeart;
            hearts[1].GetComponent<Image>().sprite = fullHeart;
            hearts[2].GetComponent<Image>().sprite = emptyHeart;
        }

        if (playerHp.Value == 5)
        {
            hearts[0].GetComponent<Image>().sprite = fullHeart;
            hearts[1].GetComponent<Image>().sprite = fullHeart;
            hearts[2].GetComponent<Image>().sprite = halfHeart;
        }

        if (playerHp.Value == 6)
        {
            hearts[0].GetComponent<Image>().sprite = fullHeart;
            hearts[1].GetComponent<Image>().sprite = fullHeart;
            hearts[2].GetComponent<Image>().sprite = fullHeart;
        }
    }
}
