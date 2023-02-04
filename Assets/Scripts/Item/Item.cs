using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itemType;
    [SerializeField]
    private ItemManager _itemManager;

    public void SetManager(ItemManager itemManager)
    {
        _itemManager = itemManager;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _itemManager.OnItemPicked(itemType);
            WeaponManager.Instance.GetWeapon(itemType);
            Destroy(this.gameObject);
        }
    }
}


