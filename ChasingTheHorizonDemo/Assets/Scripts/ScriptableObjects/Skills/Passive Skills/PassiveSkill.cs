using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive Skills/Passive", fileName = "Passive Skill")]
public class PassiveSkill : Skill
{
    protected UnitLoader unit;

    [Header("Changed Stats")]
    // increase or reduce stats (use negative numbers for reducing stats if using addition, 0 < number < 1 if multiplication)
    // set values to 0 if unchanged and addtition, 1 if multiplication

    public Statistics stats;

    [Header("Multiply")]
    public bool multiply; // uses multiplication if true, addition if not

    /*
    public void SetUnit(UnitLoader unit)
    {
        this.unit = unit;
    }
    */

    public Statistics GetStats()
    {
        // loads passives

        // if (unit == null) return;

        // if (multiply) unit.unit.statistics *= stats;
        // else unit.unit.statistics += stats;

        return stats;
    }

    /*
    public void UnloadStats()
    {
        // unloads passives
        
        if (unit == null) return;

        if (multiply) unit.unit.statistics /= stats;
        else unit.unit.statistics -= stats;
    }
    */
}
