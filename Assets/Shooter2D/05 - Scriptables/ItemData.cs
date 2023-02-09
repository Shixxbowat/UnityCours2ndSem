using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum selection type d'objet
public enum ItemType
{
    Weapon,
    Ammo,
    Object
}

// menu creation scriptable + parametres
[CreateAssetMenu(menuName = "Scriptable/ItemData", fileName = "newItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private ItemType itemType;
    public ItemType ItemType { get { return itemType; } }
    [SerializeField] private string name;
    public string Name { get { return name; }  }
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } }

    //[field: SerializeField] public Sprite Icon2 { get; private set; }
}