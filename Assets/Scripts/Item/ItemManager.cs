using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemStage itemStage;
    public ItemDataList itemDataList;
    public Dictionary<ItemType, ItemData> itemDataDictionary;
    public Dictionary<ItemType, int> hadPickedItems;
    public Transform itemSpawnTransform;

    public float layerWidth;

    public ItemType currentItemType;
    public int currentLayerIndex;
    

    // Start is called before the first frame update
    void Start()
    {
        // LayerTriggerInit
        LayerTrigger[] layerTriggers = FindObjectsOfType<LayerTrigger>();
        foreach (LayerTrigger layerTrigger in layerTriggers)
        {
            layerTrigger.SetManager(this);
        }
        GameStart();
    }

    public void GameStart()
    {
        // Item Dictionary Prefab
        itemDataDictionary = new Dictionary<ItemType, ItemData>();
        foreach (ItemData item in itemDataList.itemDatas)
        {
            // Item item = go.GetComponent<Item>();
            if (itemDataDictionary.ContainsKey(item.itemType))
            {
                Debug.LogWarning($"{item.name} 的 ItemType 與其他 Item 重複了");
            }
            else
            {
                itemDataDictionary.Add(item.itemType, item);
            }
        }
        ItemSpawn();

        // Init had pick item
        hadPickedItems = new Dictionary<ItemType, int>();
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            hadPickedItems.Add(itemType, 0);
        }
        
        
    }

    public void ItemSpawn()
    {
        float preLayerY = 0;
        foreach (Layer layer in itemStage.Layers)
        {
            foreach (ItemClip itemClip in layer.itemClips)
            {
                ItemType itemType = itemClip.itemType;
                for (int i = 0; i < itemClip.count; i++)
                {
                    Item item = Instantiate(itemDataDictionary[itemType].prefab, itemSpawnTransform).GetComponent<Item>();

                    float x = UnityEngine.Random.Range(-layerWidth, layerWidth);
                    float y = - UnityEngine.Random.Range(preLayerY, layer.maxDepth);
                    item.transform.position =  new Vector2(x, y);
                    item.SetManager(this);
                }
            }
            preLayerY = layer.maxDepth;
        }
    }

    public void OnItemPicked(ItemType itemType)
    {
        hadPickedItems[itemType] += 1;
        currentItemType = itemType;
        Debug.Log($"Pick {itemType} , current count : {hadPickedItems[itemType]}");
    }

    public void OnEnterLayer(int layerIndex)
    {
        if (layerIndex > currentLayerIndex)
        {
            currentLayerIndex = layerIndex;
        }
    }
}

[Serializable]
public enum ItemType
{
    Seed, Kirito, Trident,  Cola,
    MuscleProtein, Tentacle, Sky, Core
}
