using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puertas.Variables;


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public AudioClip itemSound;
    public GameObject itemPrefab;
}


