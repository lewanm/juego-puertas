using Puertas.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{

    [SerializeField] IntReference killCount;
    [SerializeField] GameObject spikes;

    public static ManagerGame Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        killCount.Value = 0;
        spikes.SetActive(true);

    }

    public void SumKillCount()
    {
        
        killCount.Value++;
        print(killCount.Value);
    }

    public void TurnOnSpikes()
    {
        spikes.SetActive(true);
    }

    public void TurnOffSpikes()
    {
        spikes.SetActive(false); 
    }
}
