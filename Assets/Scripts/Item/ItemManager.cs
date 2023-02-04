using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemStage itemStage;
    public List<GameObject> itemPrefabs;
    public Dictionary<ItemType, GameObject> itemPrefabDictionary;
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
        itemPrefabDictionary = new Dictionary<ItemType, GameObject>();
        foreach (GameObject go in itemPrefabs)
        {
            Item item = go.GetComponent<Item>();
            if (itemPrefabDictionary.ContainsKey(item.itemType))
            {
                Debug.LogWarning($"{item.name} 的 ItemType 與其他 Item 重複了");
            }
            else
            {
                itemPrefabDictionary.Add(item.itemType, go);
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
        foreach (Layer layer in itemStage.Layers)
        {
            foreach (ItemClip itemClip in layer.itemClips)
            {
                ItemType itemType = itemClip.itemType;
                for (int i = 0; i < itemClip.count; i++)
                {
                    Item item = Instantiate(itemPrefabDictionary[itemType], itemSpawnTransform).GetComponent<Item>();

                    float x = UnityEngine.Random.Range(0f, 5f);
                    x = layerWidth * (x * x) / 25;
                    if (UnityEngine.Random.Range(0, 2) == 0)
                    {
                        x *= -1;
                    }
                    float y = - UnityEngine.Random.Range(layer.minDepth, layer.maxDepth);
                    item.transform.position =  new Vector2(x, y);
                    item.SetManager(this);
                }
            }
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
    Water, Kirito
}
