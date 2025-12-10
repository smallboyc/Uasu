using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Health")]
public class Consumable : ItemObject
{
    public int restoreHealthValue;

    public void Awake()
    {
        type = ItemType.Consumable;
    }
}
