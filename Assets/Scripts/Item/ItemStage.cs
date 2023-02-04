using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "ItemStage", menuName= "Create ItemStage")]
public class ItemStage : ScriptableObject
{
    public List<Layer> Layers;
    
}

[Serializable]
public class Layer
{
    public int layerIndex;
    public int minDepth;
    public int maxDepth;
    public List<ItemClip> itemClips;
}

[Serializable]
public class ItemClip
{
    public ItemType itemType;
    public int count;
    
}
