using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemType
{
    Consumable,
    Equipment,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    
}
