using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemAsset", menuName = "ScriptableObjects/ItemAsset", order = 1)]
public class ItemAsset : ScriptableObject
{
    [field: SerializeField] public string Text { get; private set; }
    [field: SerializeField] public Texture2D Preview { get; private set; }
    [field: SerializeField] public GameObject Content { get; private set; }
}
