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

    bool finded = false;
    private void Update()
    {
        CheckAlpha();
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
        if (!finded)
        {
            if (distance <= GameManager.Instance.itemShowDistance)
            {
                float alpha = 1 - (distance / GameManager.Instance.itemShowDistance);
                Color temp = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, alpha);
                mySprite.color = temp;
                if (alpha > 0.6f)
                    finded = true;
            }
            else
            {
                Color temp = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 0f);
                mySprite.color = temp;
            }
        }
        else
        {
            Color temp = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 1f);
            mySprite.color = temp;
        }


    }

}


