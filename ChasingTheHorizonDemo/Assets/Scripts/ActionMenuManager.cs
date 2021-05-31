using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button actionMenuButton;
    [SerializeField]
    private Button inventoryButton;

    CursorController cursor;

    public GameObject weaponStatMenu;
    public GameObject inventoryMenu;
    public GameObject combatPreview;
    public Transform inventorySpawn;

    [Header("Inventory Slots")]
    public Text[] itemSlots;

    [Header("Weapon Stats")]
    public Text weaponName;
    public Text might;
    public Text hit;
    public Text crit;
    public Text range;
    public Text weight;

    [Header("Combat Preview Stats")]
    public Image attackerPortrait;
    public Image defenderPortrait;
    public Text attackerHP;
    public Text attackerDamage;
    public Text attackerHit;
    public Text attackerCrit;
    public Text defenderHP;
    public Text defenderDamage;
    public Text defenderHit;
    public Text defenderCrit;

    private void Start()
    {
        actionMenuButton.Select();
        cursor = FindObjectOfType<CursorController>();
    }

    public void Attack()
    {
        if(cursor.selectedUnit.target != null)
        {
            combatPreview.SetActive(true);
            FillCombatPreview();
        }
    }

    public void Items()
    {
        inventoryMenu.SetActive(true);
        inventoryMenu.transform.position = inventorySpawn.position;
        inventoryButton.Select();
        FillInventory();
    }

    public void Wait()
    {
        cursor.selectedUnit.Rest();
        cursor.selectedUnit = null;
        gameObject.SetActive(false);
        cursor.ActionMenuCursor = false;
        cursor.MapCursor = true;
    }

    private void FillInventory()
    {
        for (int i = 0; i < cursor.selectedUnit.inventory.inventory.Count; i++)
        {
            itemSlots[i].GetComponent<Text>().text = cursor.selectedUnit.inventory.inventory[i].name;
        }
    }

    public void InventoryBackButton()
    {
        inventoryMenu.SetActive(false);
        weaponStatMenu.SetActive(false);
        actionMenuButton.Select();
    }

    public void UseItem(int item)
    {
        if(cursor.selectedUnit.inventory.inventory[item].type == ItemType.Consumable)
        {
            cursor.selectedUnit.inventory.inventory[item].Use();
            cursor.selectedUnit.inventory.inventory.RemoveAt(item);
            itemSlots[item].text = "";
        }
        else if(cursor.selectedUnit.inventory.inventory[item].type == ItemType.Weapon)
        {
            weaponStatMenu.SetActive(true);
            cursor.selectedUnit.inventory.inventory[item].Equip(cursor.selectedUnit);
            FillWeaponData(cursor.selectedUnit.equippedWeapon);
        }
    }

    private void FillWeaponData(Weapon weapon)
    {
        weaponName.text = weapon.name;
        might.text = "Mt:" + weapon.might.ToString();
        hit.text = "Hit:" + weapon.hit.ToString();
        crit.text = "Crit:" + weapon.crit.ToString();
        weight.text = "Wt:" + weapon.weight.ToString();
        range.text = "Range:" + weapon.range.ToString();
    }

    private void FillCombatPreview()
    {
        attackerPortrait.sprite = cursor.selectedUnit.unit.sprite;
        attackerHP.text = cursor.selectedUnit.hp.ToString();
        attackerDamage.text = CombatManager.instance.Hit(cursor.selectedUnit, cursor.selectedUnit.target).ToString();
        attackerHit.text = (cursor.selectedUnit.hit - cursor.selectedUnit.target.avoid).ToString();
        attackerCrit.text = cursor.selectedUnit.crit.ToString();

        defenderPortrait.sprite = cursor.selectedUnit.target.unit.sprite;
        defenderHP.text = cursor.selectedUnit.target.hp.ToString();
        defenderDamage.text = CombatManager.instance.Hit(cursor.selectedUnit.target, cursor.selectedUnit).ToString();
        defenderHit.text = (cursor.selectedUnit.target.hit - cursor.selectedUnit.avoid).ToString();
        defenderCrit.text = cursor.selectedUnit.target.crit.ToString();
    }

    public void PreviewBackButton()
    {
        combatPreview.SetActive(false);
        actionMenuButton.Select();
    }

    public void PreviewAttackButton()
    {
        CombatManager.instance.EngageAttack(cursor.selectedUnit, cursor.selectedUnit.target);
        cursor.selectedUnit.Rest();
        cursor.selectedUnit = null;
        combatPreview.SetActive(false);
        gameObject.SetActive(false);
        cursor.ActionMenuCursor = false;
        cursor.MapCursor = true;
    }
}
