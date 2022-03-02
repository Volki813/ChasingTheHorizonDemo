using UnityEngine;
using UnityEngine.UI;

//Holds the data for items in the currently selected units inventory
//Used in conjuction with the InventoryMenu script

namespace InventorySystem
{
    public class InventorySlot : MonoBehaviour
    {
        public Item item = null;
        public GameObject equippedIcon = null;
        private Button slotButton = null;
        private Image slotIcon = null;        

        private void Awake()
        {
            if(GetComponent<Button>()){
                slotButton = GetComponent<Button>();
            }
            slotIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
            equippedIcon = gameObject.transform.GetChild(1).gameObject;
        }

        private void OnDisable()
        {
            equippedIcon.SetActive(false);
        }

        public void FillSlot()
        {
            slotIcon.sprite = item.itemIcon;
            slotIcon.color = new Color32(255, 255, 255, 255);
        }
        public void ClearSlot()
        {
            if(item){
                item = null;
            }
            if(slotIcon){
                slotIcon.sprite = null;
                slotIcon.color = new Color32(255, 255, 255, 0);
            }
        }
    }
}

