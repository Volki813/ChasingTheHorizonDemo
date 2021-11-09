using UnityEngine;
using UnityEngine.UI;

//Manages the all the UI on screen
//Currently manages the Tile Info UI, I may move that to it's own script we'll see
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
        AllyUnitUI();
        EnemyUnitUI();
        TileUI();
    }


    private void AllyUnitUI()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(transform.position == unit.transform.position && unit.unit.allyUnit)
            {
                unitName.text = unit.unit.unitName;
                unitHP.text = "HP: " + unit.currentHealth.ToString() + "/" + unit.unit.statistics.health.ToString();
                unitEXP.text = "Exp: " + unit.unit.exp.ToString();
                unitLevel.text = "Lvl: " + unit.unit.level.ToString();
                unitAgility.text = "Agl: " + unit.unit.statistics.agility.ToString();

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
                enemyName.text = unit.unit.unitName;
                enemyHP.text = "HP: " + unit.currentHealth.ToString() + "/" + unit.unit.statistics.health.ToString();
                enemyEXP.text = "Exp: " + unit.unit.exp.ToString();
                enemyLevel.text = "Lvl: " + unit.unit.level.ToString();
                enemyAgility.text = "Agl: " + unit.unit.statistics.agility.ToString();

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
