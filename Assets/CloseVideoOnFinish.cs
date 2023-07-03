using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CloseVideoOnFinish : MonoBehaviour
{
    VideoPlayer video;
    Transform correccion;
    float timer = 0f;
    int nextScene = 1;

    float timerPrueba = 0f;
    void Start()
    {
        correccion = transform.GetChild(0);
        video = GetComponent<VideoPlayer>();
        video.loopPointReached += CheckOver;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space)) {
            SkipScene();
        }

        //SkipScene();

        timerPrueba += Time.deltaTime;
        Debug.Log(timerPrueba);

        if (timerPrueba >= 10.73f) correccion.gameObject.SetActive(true);//se va a los 14
        if (timerPrueba >= 14.21f) correccion.gameObject.SetActive(false);
    }

    void CheckOver(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }

    void SkipScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
