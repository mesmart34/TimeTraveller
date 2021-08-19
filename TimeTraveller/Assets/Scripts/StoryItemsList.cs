using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryItemsList : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI[] _items;
    [SerializeField] private Inventory _inventory;

    private void Start() {
        _inventory.OnInventoryItemAdded += OnItemAdded;
    }

    public void OnItemAdded(ItemBase item)
    {
        _items[item.Id].fontStyle = FontStyles.Strikethrough;
    }

    private void OnDestroy() {
        _inventory.OnInventoryItemAdded -= OnItemAdded;
    }

}
