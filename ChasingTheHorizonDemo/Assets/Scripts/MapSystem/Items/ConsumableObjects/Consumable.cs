using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable")]
public class Consumable : Item
{
    public int ID;
    public void Awake()
    {
        type = ItemType.Consumable;
    }

    public override void Use(UnitLoader unit)
    {
        ItemLibrary.instance.UseItem(ID);
    }
}
