using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
public class ItemBase : ScriptableObject
{
    public string Name;
    public Sprite Picture;
    public int Id;

    public GameEvent destroyEvent;
}
