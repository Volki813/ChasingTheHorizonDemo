using UnityEngine;

//I realize now that consumables can be more than just healing items so Ill probably change this to be specifically for healing items
[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable")]
public class Consumable : Item
{
    [Header("Consumable Attributes")]
    public int healValue = 0;
}
