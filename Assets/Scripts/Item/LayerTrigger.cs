using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    public int index;
    private ItemManager _itemManager;

    public void SetManager(ItemManager itemManager)
    {
        _itemManager = itemManager;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _itemManager.OnEnterLayer(index);
        }
    }
}
