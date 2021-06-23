using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Ally Unit UI")]
    public Image unitPortrait;
    public Text unitName;
    public Text unitHP;
    public Text unitLevel;
    public Text unitEXP;
    public Text unitAgility;

    public Text unitStrength;
    public Text unitMagic;
    public Text unitDefense;
    public Text unitResistance;

    [Header("Enemy UI Info")]
    public Image enemyPortrait;
    public Text enemyName;
    public Text enemyHP;
    public Text enemyLevel;
    public Text enemyEXP;
    public Text enemyAgility;

    public Text enemyStrength;
    public Text enemyMagic;
    public Text enemyDefense;
    public Text enemyResistance;

    [Header("Tile UI Info")]
    public Text tileName;
    public Text tileCost;

    private void Update()
    {
        UnitUI();
        EnemyUnitUI();
        TileUI();
    }

    private void UnitUI()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(transform.position == unit.transform.position && unit.unit.allyUnit)
            {
                unitPortrait.sprite = unit.unit.sprite;
                unitName.text = unit.name;
                unitHP.text = "Health: " + unit.currentHealth.ToString() + " / " + unit.unit.statistics.health.ToString();
                unitEXP.text = "EXP: " + unit.unit.exp.ToString();
                unitLevel.text = "Level: " + unit.unit.level.ToString();
                unitAgility.text = "Agility: " + unit.unit.statistics.agility.ToString();

                unitStrength.text = "Str: " + unit.unit.statistics.strength.ToString();
                unitMagic.text = "Mag: " + unit.unit.statistics.magic.ToString();
                unitDefense.text = "Def: " + unit.unit.statistics.defense.ToString();
                unitResistance.text = "Res: " + unit.unit.statistics.resistance.ToString();
            }
        }
    }

    private void EnemyUnitUI()
    {
        foreach (UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if (transform.position == unit.transform.position && !unit.unit.allyUnit)
            {
                enemyPortrait.sprite = unit.unit.sprite;
                enemyName.text = unit.name;
                enemyHP.text = "Health: " + unit.currentHealth.ToString() + " / " + unit.unit.statistics.health.ToString();
                enemyEXP.text = "EXP: " + unit.unit.exp.ToString();
                enemyLevel.text = "Level: " + unit.unit.level.ToString();
                enemyAgility.text = "Agility: " + unit.unit.statistics.agility.ToString();

                enemyStrength.text = "Str: " + unit.unit.statistics.strength.ToString();
                enemyMagic.text = "Mag: " + unit.unit.statistics.magic.ToString();
                enemyDefense.text = "Def: " + unit.unit.statistics.defense.ToString();
                enemyResistance.text = "Res: " + unit.unit.statistics.resistance.ToString();
            }
        }
    }

    private void TileUI()
    {
        foreach (TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if (transform.position == tile.transform.position)
            {
                tileName.text = tile.tileName;
                tileCost.text = tile.tileCost.ToString();
            }
        }
    }
}
