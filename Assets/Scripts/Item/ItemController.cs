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

    public ItemType testItemType;
    [ContextMenu("test")]
    public void Test()
    {
        ShowPickItemPanel(testItemType);
    }

    public void ShowPickItemPanel(ItemType itemType)
    {
        ItemData itemData = itemManager.itemDataDictionary[itemType];
        itemImage.sprite = itemData.sprite;
        itemNameText.text = itemData.name;
        itemDescriptionText.text = itemData.description;
        
        pickItemPanel.SetActive(true);
        pickItemPanelAnimator.SetTrigger("Show");
    }
}
