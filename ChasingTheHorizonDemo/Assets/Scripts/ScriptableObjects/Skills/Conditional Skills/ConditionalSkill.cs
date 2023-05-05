using UnityEngine;

public class ConditionalSkill : Skill
{
    protected UnitLoader unit;
    protected bool condition = false;

    [Header("Changed Stats")]
    // increase or reduce stats (use negative numbers for reducing stats if using addition, 0 < number < 1 if multiplication)
    // set values to 0 if unchanged and addtition, 1 if multiplication
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

    public virtual bool CheckCondition() // will be overriden by subclasses (don't use this class, since the condition isn't specified)
    {
        return condition;
    }
}
