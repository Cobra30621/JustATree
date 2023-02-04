using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "ItemDataList", menuName= "Create ItemDataList")]
public class ItemDataList : ScriptableObject
{
    public List<ItemData> itemDatas;
}

[Serializable]
public class ItemData
{
    public ItemType itemType;
    public string name;
    public string description;
    public GameObject prefab;
    public Sprite sprite;
}