using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void Attack()
    {
        if (timer > RandomSecondsBetween(3,6))
        {
            timer = -10;
        }
    }

    float RandomSecondsBetween(int n1, int n2)
    {
        return Random.Range(n1, n2);
    }

    //Hacer que no se pueda mover fuera de los bordes
    public void Attack1()
    {

    }

    public void Attack2()
    {

    }
}
