using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(new Vector3(0.001f, 0, 0) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
