using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public static EndingAudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
