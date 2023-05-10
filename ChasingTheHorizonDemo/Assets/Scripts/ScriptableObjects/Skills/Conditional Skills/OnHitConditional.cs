using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Conditional Skills/On Hit Condition", fileName = "Conditional Skill")]
public class OnHitConditional : ConditionalSkill
{
    [SerializeField] private bool[] usedStat = new bool[9]; // represents each stat for calculation, tick for the desired stat, i.e. health = element 0, defense = element 3, etc.
    [Range(1, 100)][SerializeField] private int divideValue = 0; // the base percentage, the desired stat is gonna get divided with

    public override bool CheckCondition(UnitLoader unit, float randomNumber)
    {
        for (int i = 0; i < usedStat.Length; i++)
        {
            if (i == 0 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.health / divideValue;
            }
            else if (i == 1 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.strength / divideValue;
            }
            else if (i == 2 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.magic / divideValue;
            }
            else if (i == 3 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.defense / divideValue;
            }
            else if (i == 4 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.resistance / divideValue;
            }
            else if (i == 5 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.proficiency / divideValue;
            }
            else if (i == 6 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.motivation / divideValue;
            }
            else if (i == 7 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.agility / divideValue;
            }
            else if (i == 8 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.movement / divideValue;
            }
        }
        return false;
    }
}
