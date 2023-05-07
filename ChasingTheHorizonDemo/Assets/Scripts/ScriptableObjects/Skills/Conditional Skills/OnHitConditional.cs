using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Conditional Skills/On Hit Condition", fileName = "Conditional Skill")]
public class OnHitConditional : ConditionalSkill
{
    [SerializeField] private bool[] usedStat = new bool[9]; // represents each stat for calculation, tick for the desired stat, i.e. health = element 0, defense = element 3, etc.
    [Range(1, 100)][SerializeField] private int percentage = 0; // the base percentage, the desired stat is gonna get divided with

    public override bool CheckCondition(UnitLoader unit, float randomNumber)
    {
        for (int i = 0; i < usedStat.Length; i++)
        {
            if (i == 0 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.health / percentage;
            }
            else if (i == 1 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.strength / percentage;
            }
            else if (i == 2 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.magic / percentage;
            }
            else if (i == 3 && usedStat[i])
            {
                Debug.Log(randomNumber);

                return randomNumber <= unit.unit.statistics.defense / percentage;
            }
            else if (i == 4 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.resistance / percentage;
            }
            else if (i == 5 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.proficiency / percentage;
            }
            else if (i == 6 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.motivation / percentage;
            }
            else if (i == 7 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.agility / percentage;
            }
            else if (i == 8 && usedStat[i])
            {
                return randomNumber <= unit.unit.statistics.movement / percentage;
            }
        }
        return false;
    }
}
