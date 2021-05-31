using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
public class Weapon : Item
{
    public int might;
    public int hit;
    public int crit;
    public int range;
    public int weight;

    public void Awake()
    {
        type = ItemType.Weapon;
    }

    public override void Equip(UnitLoader unit)
    {
        unit.equippedWeapon = this;
    }
}
