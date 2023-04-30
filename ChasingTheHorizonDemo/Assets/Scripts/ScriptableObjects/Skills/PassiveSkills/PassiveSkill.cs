
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive Skill", fileName = "Passive Skill")]
public class PassiveSkill : Skill
{
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
}
