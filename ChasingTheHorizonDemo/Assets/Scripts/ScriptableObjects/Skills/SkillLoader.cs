using UnityEngine;

public class SkillLoader : MonoBehaviour
{

    // place on unit and then drag passives here
    [SerializeField] private PassiveSkill[] passives = null; // order is important if multiply skills are included: if addition skill is placed before multiply skill, the stat increase will multiply too, otherwise, it won't
    [SerializeField] private LikelihoodPassive[] likelihoodPassives = null;

    private void OnEnable()
    {
        if (passives != null)
        {
            foreach (PassiveSkill passive in passives)
            {
                passive.SetUnit(GetComponent<UnitLoader>());
                passive.LoadStats();
            }
        }

        if (likelihoodPassives != null)
        {
            foreach (LikelihoodPassive likelihoodPassive in likelihoodPassives)
            {
                likelihoodPassive.SetUnit(GetComponent<UnitLoader>());
                likelihoodPassive.LoadStats();
                likelihoodPassive.IncreaseLikelihoodOfBeingTargeted();
            }
        }
    }

    private void OnDisable()
    {
        if (passives != null)
        {
            foreach (PassiveSkill passive in passives)
            {
                passive.SetUnit(GetComponent<UnitLoader>());
                passive.UnloadStats();
            }
        }

        if (likelihoodPassives != null)
        {
            foreach (LikelihoodPassive likelihoodPassive in likelihoodPassives)
            {
                likelihoodPassive.SetUnit(GetComponent<UnitLoader>());
                likelihoodPassive.UnloadStats();
                likelihoodPassive.DecreaseLikelihoodOfBeingTargeted();
            }
        }

    }
}
