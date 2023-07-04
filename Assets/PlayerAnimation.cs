using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator anim;
    [SerializeField] GameObject tuki;
    int counter = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void StartAnimation()
    {
        anim.SetTrigger("bebe");
    }

    public void StartTextAnimation()
    {
        if (counter == 0)
        {
            tuki.GetComponent<TextosTuki>().StartAnimation();
        }
        counter++; 
        
    }
}
