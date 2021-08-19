using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    public List<ItemBase> Items;
    public event Action<ItemBase> OnInventoryItemAdded;

    public void Clear()
    {
        Items.Clear();
    }

    public void Add(ItemBase item)
    {
        Items.Add(item);
        OnInventoryItemAdded?.Invoke(item);
    }

    public bool HasItem(string Name)
    {
        foreach(var item in Items)
            if(item.Name == Name)
                return true;
        return false;
    }

}
