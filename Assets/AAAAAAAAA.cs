using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AAAAAAAAA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadCreditsNumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadCreditsNumerator()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }
}
