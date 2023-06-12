using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        CheckPosition();
    }

    void CheckPosition()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Ceil(Mathf.Abs(transform.position.y) *10);
    }



}
