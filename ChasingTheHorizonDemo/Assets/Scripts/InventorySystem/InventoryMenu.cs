using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Manages the Inventory Menu
//Works in conjunction with the InventorySlot script
//Displays the items of the selected unit in each of the inventory slots
//Also displays item information

namespace InventorySystem
{
    public class InventoryMenu : MonoBehaviour
    {
        [SerializeField] private CursorController cursor = null;
        [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();

        [Header("Item Preview")]
        [SerializeField] private Text itemName = null;
        [SerializeField] private Image itemIcon = null;
        [SerializeField] private Text itemDescription = null;

        [Header("Weapon Preview")]
        [SerializeField] private Text weaponMight = null;
        [SerializeField] private Text weaponHit = null;
        [SerializeField] private Text weaponCrit = null;
        [SerializeField] private Text weaponRange = null;
        [SerializeField] private Text weaponWeight = null;

        private void Awake()
        {
            cursor = FindObjectOfType<CursorController>();
        }

        private void Start()
        {
            SetSlots();
        }

        private void Update()
        {
            ItemPreview();
        }

        private void OnEnable()
        {
            Invoke("FillSlots", 0.01f);
            Invoke("ResetEquippedWeapon", 0.01f);
        }

        private void OnDisable()
        {
            ClearSlots();
            ClearItemPreview();
        }

        public void UseItem(InventorySlot item)
        {
            //check what kind of item you have
            if (item.item.type == ItemType.Weapon)
            {
                cursor.selectedUnit.equippedWeapon = null;
                cursor.selectedUnit.equippedWeapon = (Weapon)item.item;
                Invoke("ResetEquippedWeapon", 0.01f);
            }
            else if (item.item.type == ItemType.Consumable)
            {
                Consumable consumable = (Consumable)item.item;
                if (cursor.selectedUnit.currentHealth == cursor.selectedUnit.unit.statistics.health)
                {
                    return;
                }
                else if (cursor.selectedUnit.currentHealth + consumable.healValue > cursor.selectedUnit.unit.statistics.health)
                {
                    cursor.selectedUnit.currentHealth += cursor.selectedUnit.unit.statistics.health - cursor.selectedUnit.currentHealth;
                }
                else
                {
                    cursor.selectedUnit.currentHealth += consumable.healValue;
                }
                cursor.selectedUnit.inventory.inventory.Remove(consumable);
                ClearSlots();
                Invoke("FillSlots", 0.01f);
            }
        }

        private void ItemPreview()
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<InventorySlot>() && EventSystem.current.currentSelectedGameObject.GetComponent<InventorySlot>().item != null)
            {
                InventorySlot highlightedSlot = EventSystem.current.currentSelectedGameObject.GetComponent<InventorySlot>();

                if (highlightedSlot.item.type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)highlightedSlot.item;

                    itemName.text = highlightedSlot.item.itemName;
                    itemIcon.sprite = highlightedSlot.item.itemIcon;
                    itemDescription.text = highlightedSlot.item.itemDescription;

                    weaponMight.text = "Might: " + weapon.might.ToString();
                    weaponHit.text = "Hit: " + weapon.hit.ToString();
                    weaponCrit.text = "Crit: " + weapon.crit.ToString();
                    weaponRange.text = "Range: " + weapon.range.ToString();
                    weaponWeight.text = "Weight: " + weapon.weight.ToString();
                }
                else
                {
                    ClearItemPreview();
                    itemName.text = highlightedSlot.item.itemName;
                    itemIcon.sprite = highlightedSlot.item.itemIcon;
                    itemDescription.text = highlightedSlot.item.itemDescription;
                }
            }
            else
            {
                ClearItemPreview();
            }
        }
        private void ClearItemPreview()
        {
            itemName.text = null;
            itemIcon.sprite = null;
            itemDescription.text = null;
            weaponMight.text = null;
            weaponHit.text = null;
            weaponCrit.text = null;
            weaponRange.text = null;
            weaponWeight.text = null;
        }
        private void SetSlots()
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).GetComponent<InventorySlot>())
                {
                    inventorySlots.Add(gameObject.transform.GetChild(i).GetComponent<InventorySlot>());
                }
            }
        }
        private void FillSlots()
        {
            EventSystem.current.SetSelectedGameObject(inventorySlots[0].gameObject);
            for (int i = 0; i < cursor.selectedUnit.inventory.inventory.Count; i++)
            {
                inventorySlots[i].item = cursor.selectedUnit.inventory.inventory[i];
                inventorySlots[i].FillSlot();
            }            
        }
        private void ResetEquippedWeapon()
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                slot.equippedIcon.SetActive(false);
            }
            foreach (InventorySlot slot in inventorySlots)
            {
                if (cursor.selectedUnit.equippedWeapon == slot.item)
                {
                    slot.equippedIcon.SetActive(true);
                    return;
                }
            }
        }
        private void ClearSlots()
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}

