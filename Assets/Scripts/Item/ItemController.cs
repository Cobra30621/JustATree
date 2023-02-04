using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [Header("Pick Item Panel")] 
    public Animator pickItemPanelAnimator;

    public GameObject pickItemPanel;
    public Image itemImage;
    public Text itemNameText, itemDescriptionText;

    public ItemManager itemManager;

    [Header("Current Item")] 
    public Image currentItemImage;
    public Text currentItemText;

    public ItemType testItemType;
    [ContextMenu("test")]
    public void Test()
    {
        ShowPickItemPanel(testItemType);
    }

    public void ShowPickItemPanel(ItemType itemType)
    {
        if (itemType == ItemType.Sky)
        {
            return;
        }
        ItemData itemData = itemManager.itemDataDictionary[itemType];
        itemImage.sprite = itemData.sprite;
        itemNameText.text = itemData.name;
        itemDescriptionText.text = itemData.description;
        
        currentItemImage.sprite = itemData.sprite;
        currentItemText.text = itemData.name;
        
        pickItemPanelAnimator.SetTrigger("Show");
    }
}
