using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    Transform spikeHitbox;

    void Start()
    {
        spikeHitbox = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (isBetween(7, 11))
        {
            spikeHitbox.gameObject.SetActive(true);

        }
        else spikeHitbox.gameObject.SetActive(false);

    }

    bool isBetween(int min, int max)
    {
        string spriteName = GetComponent<SpriteRenderer>().sprite.ToString();

        bool isBetween = false;

        for (int i = min; i <= max; i++)
        {
            isBetween = isBetween || spriteName.Contains($"{i}");
        }

        return isBetween;
    }
}
