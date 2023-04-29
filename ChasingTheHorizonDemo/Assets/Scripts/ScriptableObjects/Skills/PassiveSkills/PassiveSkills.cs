
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive Skill")]
public class PassiveSkills : ScriptableObject
{
    // increase or reduce stats (use negative numbers for reducing stats
    // leave at 0 if stats should stay unchanged
    public int healthAmount;
    public int strengthAmount;
    public int magicAmount;
    public int defenseAmount;
    public int resistanceAmount;
    public int proficiencyAmount;
    public int motivationAmount;
    public int agilityAmount;
    public int schmovementAmount;
}
