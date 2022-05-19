using System.Collections.Generic;
using UnityEngine;
//Simple Inventory that holds up to 5 items, item limit can be changed
//Inventories should be given to all AllyUnits and to EnemyUnits when appropriate

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> inventory = new List<Item>();

        public void AddItem(Item item)
        {
            if (inventory.Count < 5)
            {
                inventory.Add(item);
            }
        }
    }
}

