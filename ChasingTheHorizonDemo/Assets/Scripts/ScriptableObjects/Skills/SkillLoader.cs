using UnityEngine;

public class SkillLoader : MonoBehaviour
{

    // place on unit and then drag passives here
    [SerializeField] private PassiveSkills[] passives;

    private void OnEnable()
    {
        // loads passives

        UnitLoader unit = GetComponent<UnitLoader>();

        foreach (PassiveSkills passive in passives)
        {
            if (unit == null) break;

            unit.unit.statistics.health += passive.healthAmount;
            unit.unit.statistics.strength += passive.strengthAmount;
            unit.unit.statistics.magic += passive.magicAmount;
            unit.unit.statistics.defense += passive.defenseAmount;
            unit.unit.statistics.resistance += passive.resistanceAmount;
            unit.unit.statistics.proficiency += passive.proficiencyAmount;
            unit.unit.statistics.motivation += passive.motivationAmount;
            unit.unit.statistics.agility += passive.agilityAmount;
            unit.unit.statistics.movement += passive.schmovementAmount;
        }
    }

    private void OnDisable()
    {
        // unloads passives

        UnitLoader unit = GetComponent<UnitLoader>();

        foreach (PassiveSkills passive in passives)
        {
            if (unit == null) break;

            unit.unit.statistics.health -= passive.healthAmount;
            unit.unit.statistics.strength -= passive.strengthAmount;
            unit.unit.statistics.magic -= passive.magicAmount;
            unit.unit.statistics.defense -= passive.defenseAmount;
            unit.unit.statistics.resistance -= passive.resistanceAmount;
            unit.unit.statistics.proficiency -= passive.proficiencyAmount;
            unit.unit.statistics.motivation -= passive.motivationAmount;
            unit.unit.statistics.agility -= passive.agilityAmount;
            unit.unit.statistics.movement -= passive.schmovementAmount;

        }
    }
}
