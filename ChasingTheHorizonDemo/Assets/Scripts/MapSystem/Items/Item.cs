using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Weapon,
}

public abstract class Item : ScriptableObject
{
    public ItemType type;
    public string description;

    public virtual void Use()
    {
        
    }

    public virtual void Equip(UnitLoader unit)
    {
        
    }
}
