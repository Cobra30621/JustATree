using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itemType;
    [SerializeField]
    private ItemManager _itemManager;
    [SerializeField]
    SpriteRenderer mySprite;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckAlpha();
        }
    }
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

    private void CheckAlpha()
    {
        float distance = GameManager.Instance.GetDistanceFromHead(this.transform);
        Debug.Log("¶ZÂ÷head:" + distance);
    }

}


