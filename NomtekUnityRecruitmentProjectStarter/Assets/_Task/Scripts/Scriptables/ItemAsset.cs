using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemAsset", menuName = "ScriptableObjects/ItemAsset", order = 1)]
public class ItemAsset : ScriptableObject
{
    [field: SerializeField] public string Text { get;  set; }
    [field: SerializeField] public Texture2D Preview { get;  set; }
    [field: SerializeField] public GameObject Content { get;  set; }
}
