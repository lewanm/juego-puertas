using Puertas.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{

    [SerializeField] IntReference killCount;
    [SerializeField] GameObject spikes;
    [SerializeField] GameObject devMenu;
    [SerializeField] GameObject[] hitBoxesPrefabs;

    bool devMenuIsOpen = false;


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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            devMenuIsOpen = !devMenuIsOpen;
            devMenu.SetActive(devMenuIsOpen);
        }
    }

    private void Start()
    {
        killCount.Value = 0;
        spikes.SetActive(true);
        devMenu.SetActive(false);
        ShowAllHitboxes(false);

    }

    public void SumKillCount()
    {
        killCount.Value++;
    }

    public void SwitchSpikesState(bool state)
    {
        spikes.SetActive(state);
    }

    public void ShowAllHitboxes(bool state)
    {
        foreach (var prefab in hitBoxesPrefabs)
        {
            prefab.GetComponent<SpriteRenderer>().enabled = state;
        }
    }
}
