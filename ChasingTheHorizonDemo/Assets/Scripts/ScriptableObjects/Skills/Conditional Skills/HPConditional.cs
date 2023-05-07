using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Conditional Skills/HP Condition", fileName = "Conditional Skill")]
public class HPConditional : ConditionalSkill
{

    [Header("HP Percentage")]
    [Range(0, 100)][SerializeField] private int hpPercentage = 0;
    [SerializeField] private bool greaterThan = false; // tick if the conditional should be active ABOVE the specified hp percentage

    public override bool CheckCondition(UnitLoader unit)
    {
        int percentageToCheck = (int)((unit.currentHealth * 100) / unit.unit.statistics.health);
        if (greaterThan) return hpPercentage < percentageToCheck;
        else return hpPercentage > percentageToCheck;
    }
}
