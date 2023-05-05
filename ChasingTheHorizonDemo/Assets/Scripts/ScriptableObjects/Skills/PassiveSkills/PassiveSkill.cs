using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive Skills/Passive", fileName = "Passive Skill")]
public class PassiveSkill : Skill
{
    protected UnitLoader unit;

    [Header("Changed Stats")]
    // increase or reduce stats (use negative numbers for reducing stats if using addition, 0 < number < 1 if multiplication)
    // set values to 0 if unchanged and addtition, 1 if multiplication
    public int healthAmount;
    public int strengthAmount;
    public int magicAmount;
    public int defenseAmount;
    public int resistanceAmount;
    public int proficiencyAmount;
    public int motivationAmount;
    public int agilityAmount;
    public int schmovementAmount;

    [Header("Multiply")]
    public bool multiply; // uses multiplication if true, addition if not

    public void SetUnit(UnitLoader unit)
    {
        this.unit = unit;
    }

    public void LoadStats()
    {
        // loads passives

        if (unit == null) return;

        if (multiply)
        {
            unit.unit.statistics.health *= healthAmount;
            unit.unit.statistics.strength *= strengthAmount;
            unit.unit.statistics.magic *= magicAmount;
            unit.unit.statistics.defense *= defenseAmount;
            unit.unit.statistics.resistance *= resistanceAmount;
            unit.unit.statistics.proficiency *= proficiencyAmount;
            unit.unit.statistics.motivation *= motivationAmount;
            unit.unit.statistics.agility *= agilityAmount;
            unit.unit.statistics.movement *= schmovementAmount;
        }
        else
        {
            unit.unit.statistics.health += healthAmount;
            unit.unit.statistics.strength += strengthAmount;
            unit.unit.statistics.magic += magicAmount;
            unit.unit.statistics.defense += defenseAmount;
            unit.unit.statistics.resistance += resistanceAmount;
            unit.unit.statistics.proficiency += proficiencyAmount;
            unit.unit.statistics.motivation += motivationAmount;
            unit.unit.statistics.agility += agilityAmount;
            unit.unit.statistics.movement += schmovementAmount;

        }
    }

    public void UnloadStats()
    {
        // unloads passives
        if (unit == null) return;

        if (multiply)
        {
            unit.unit.statistics.health /= healthAmount;
            unit.unit.statistics.strength /= strengthAmount;
            unit.unit.statistics.magic /= magicAmount;
            unit.unit.statistics.defense /= defenseAmount;
            unit.unit.statistics.resistance /= resistanceAmount;
            unit.unit.statistics.proficiency /= proficiencyAmount;
            unit.unit.statistics.motivation /= motivationAmount;
            unit.unit.statistics.agility /= agilityAmount;
            unit.unit.statistics.movement /= schmovementAmount;
        }
        else
        {
            unit.unit.statistics.health -= healthAmount;
            unit.unit.statistics.strength -= strengthAmount;
            unit.unit.statistics.magic -= magicAmount;
            unit.unit.statistics.defense -= defenseAmount;
            unit.unit.statistics.resistance -= resistanceAmount;
            unit.unit.statistics.proficiency -= proficiencyAmount;
            unit.unit.statistics.motivation -= motivationAmount;
            unit.unit.statistics.agility -= agilityAmount;
            unit.unit.statistics.movement -= schmovementAmount;
        }
    }
}
