using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("ItemManager").GetComponent<ItemManager>();
            }
            return instance;
        }
    }
    private static ItemManager instance; 
    public ItemController itemController;
    public ItemStage itemStage;
    public ItemDataList itemDataList;
    public Dictionary<ItemType, ItemData> itemDataDictionary;
    public Dictionary<ItemType, int> hadPickedItems;
    public Transform itemSpawnTransform;

    public float groundWidth;

    public ItemType currentItemType;
    public int currentLayerIndex;
    

    // Start is called before the first frame update
    void Start()
    {
        
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
        float xScale = 0.5f;
        int layerIndex = 0;
        foreach (Layer layer in itemStage.Layers)
        {
            float layerDepth = itemStage.layerDepth;
            foreach (ItemClip itemClip in layer.itemClips)
            {
                ItemType itemType = itemClip.itemType;
                for (int i = 0; i < itemClip.count; i++)
                {
                    Item item = Instantiate(itemDataDictionary[itemType].prefab, itemSpawnTransform).GetComponent<Item>();

                    float x = UnityEngine.Random.Range(- (groundWidth / 2) * xScale, (groundWidth / 2) * xScale);
                    float y = - UnityEngine.Random.Range(layerDepth * layerIndex + 0.5f, layerDepth * (layerIndex + 1) - 0.5f);
                    item.transform.position =  new Vector2(x, y);
                    item.SetManager(this);
                }
            }
            xScale += 0.1f;
            layerIndex += 1;
        }
    }

    public void OnItemPicked(ItemType itemType)
    {
        hadPickedItems[itemType] += 1;
        currentItemType = itemType;
        Debug.Log($"Pick {itemType} , current count : {hadPickedItems[itemType]}");
        itemController.ShowPickItemPanel(itemType);
        
        // Play End
        if (itemType == ItemType.Tentacle)
            GameManager.Instance.PlayEnd(EndType.Tentacle);
        if (itemType == ItemType.Core)
            GameManager.Instance.PlayEnd(EndType.Core);
        if (itemType == ItemType.Sky)
            GameManager.Instance.PlayEnd(EndType.Sky);
        if(hadPickedItems[ItemType.MuscleProtein] == 5)
            GameManager.Instance.PlayEnd(EndType.MuscleProtein);
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
