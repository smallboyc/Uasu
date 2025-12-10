using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class Equipment : ItemObject
{
    public float atackBonus;
    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
