using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] float textSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCredits();

    }

    void MoveCredits()
    {
        transform.Translate(new Vector3(0, 1, 0) * textSpeed * Time.deltaTime);
    }
}
