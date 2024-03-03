using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsAssetPack", menuName = "ScriptableObjects/ItemsAssetPack", order = 1)]
public class ItemsAssetPack : ScriptableObject
{
    [field: SerializeField] public List<ItemAsset> Items { get; private set; }
}
