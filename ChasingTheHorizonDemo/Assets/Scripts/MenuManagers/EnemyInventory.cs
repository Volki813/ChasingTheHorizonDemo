using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class EnemyInventory : MonoBehaviour
{
    private CursorController cursor = null;
    [SerializeField]
    private List<InventorySlot> items = new List<InventorySlot>();

    private void Awake()
    {
        cursor = FindObjectOfType<CursorController>();        
    }

    public void DisplayInventory(UnitLoader unit)
    {
        int amountOfItems = unit.inventory.inventory.Count;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 0; i < amountOfItems; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).GetComponent<InventorySlot>().item = unit.inventory.inventory[i];
            transform.GetChild(i).GetComponent<InventorySlot>().FillSlot();
        }
    }
}
