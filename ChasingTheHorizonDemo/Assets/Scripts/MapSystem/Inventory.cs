using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    public void AddItem(Item item)
    {
        if(inventory.Count < 5)
        {
            inventory.Add(item);
        }
    }
}
