using UnityEngine;

public class SkillLoader : MonoBehaviour
{

    // for conditionals so that they don't apply the effect every frame
    public bool hasActivated = false;

    // place on unit and then drag passives here
    [SerializeField] private PassiveSkill[] passives = null; // order is important if multiply skills are included: if addition skill is placed before multiply skill, the stat increase will multiply too, otherwise, it won't
    [SerializeField] private LikelihoodPassive[] likelihoodPassives = null;
    [SerializeField] private HPConditional[] HPConditionals = null;

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

        if (HPConditionals != null)
        {
            foreach (HPConditional HPConditional in HPConditionals)
            {
                HPConditional.SetUnit(GetComponent<UnitLoader>());
            }
        }
    }

    private void Update()
    {
        if (HPConditionals != null)
        {
            foreach (HPConditional HPConditional in HPConditionals) // didn't want to set the unit every frame
            {
                if (HPConditional.CheckCondition())
                {
                    if (!hasActivated)
                    {
                        HPConditional.LoadStats();
                        hasActivated = true;
                    }
                }
                else
                {
                    if (hasActivated)
                    {
                        HPConditional.UnloadStats();
                        hasActivated = false;
                    }
                }
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

        if (HPConditionals != null)
        {
            foreach (HPConditional HPConditional in HPConditionals)
            {
                HPConditional.SetUnit(GetComponent<UnitLoader>());
                if (hasActivated)
                {
                    HPConditional.UnloadStats();
                    hasActivated = false;
                }
            }
        }
    }
}
