using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] ItemData datas;
    [SerializeField] InventoryScriptable inventory;
    Action onItemPicked;

    // Start is called before the first frame update
    void Start()
    {
        if(datas.ItemType == ItemType.Weapon)
        {
            onItemPicked += OnWeaponPicked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnWeaponPicked()
    {
        inventory.ItemList.Add(datas);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 0)
        {
            onItemPicked?.Invoke();
            Destroy(gameObject);
        }
    }
}
