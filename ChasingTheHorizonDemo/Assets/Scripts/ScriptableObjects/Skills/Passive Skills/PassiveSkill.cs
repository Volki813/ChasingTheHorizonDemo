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

    public Statistics GetStats()
    {
        return stats;
    }
}
