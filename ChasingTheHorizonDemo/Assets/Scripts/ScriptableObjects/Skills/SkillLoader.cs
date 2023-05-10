using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class SkillLoader : MonoBehaviour
{

    // for conditionals so that they don't apply the effect every frame
    private List<bool> hasActivatedHP = new List<bool>(0);
    private List<bool> hasActivatedOnHit = new List<bool>(0);

    // place on unit and then drag passives here
    [SerializeField] private PassiveSkill[] passives = null; // order is important if multiply skills are included: if addition skill is placed before multiply skill, the stat increase will multiply too, otherwise, it won't
    [SerializeField] private LikelihoodPassive[] likelihoodPassives = null;
    [SerializeField] private HPConditional[] HPConditionals = null;
    [SerializeField] private OnHitConditional[] onHitConditionals = null;

    private bool wasHit;
    private UnitLoader unit;

    private void OnEnable()
    {
        unit = GetComponent<UnitLoader>();

        if (passives != null)
        {
            foreach (PassiveSkill passive in passives)
            {
                if (passive.multiply)
                {
                    unit.unit.statistics *= passive.GetStats();
                }
                else
                {
                    unit.unit.statistics += passive.GetStats();
                }
            }
        }

        if (likelihoodPassives != null)
        {
            foreach (LikelihoodPassive likelihoodPassive in likelihoodPassives)
            {
                if (likelihoodPassive.multiply)
                {
                    unit.unit.statistics *= likelihoodPassive.GetStats();
                }
                else
                {
                    unit.unit.statistics += likelihoodPassive.GetStats();
                }
                likelihoodPassive.IncreaseLikelihoodOfBeingTargeted();
            }
        }

        if (HPConditionals != null)
        {
            foreach (HPConditional hpConditional in HPConditionals)
            {
                hasActivatedHP.Add(false); // so that the list represents the amount of conditionals
            }
        }

        /*
        if (onHitConditionals != null)
        {
            foreach (OnHitConditional onHitConditional in onHitConditionals)
            {
                hasActivatedOnHit.Add(false);
            }
        }
        */
    }

    private void Update()
    {
        if (HPConditionals != null)
        {
            for (int i = 0; i < HPConditionals.Length; i++) // didn't want to set the unit every frame
            {
                if (!hasActivatedHP[i] && HPConditionals[i].CheckCondition(unit))
                {
                    if (HPConditionals[i].multiply)
                    {
                        unit.unit.statistics *= HPConditionals[i].GetStats();
                    }
                    else
                    {
                        unit.unit.statistics += HPConditionals[i].GetStats();
                    }
                    hasActivatedHP[i] = true;
                }
                else if (hasActivatedHP[i] && !HPConditionals[i].CheckCondition(unit))
                {
                    if (HPConditionals[i].multiply)
                    {
                        unit.unit.statistics /= HPConditionals[i].GetStats();
                    }
                    else
                    {
                        unit.unit.statistics -= HPConditionals[i].GetStats();
                    }
                    hasActivatedHP[i] = false;
                }
            }
        }

        /*
        if (onHitConditionals != null)
        {
            if (wasHit)
            {
                float randomNumber = Random.Range(1, 100);
                StartCoroutine(OnHitActions(randomNumber));
            }

        }
        */
    }

    private void OnDisable()
    {
        if (passives != null)
        {
            foreach (PassiveSkill passive in passives)
            {
                if (passive.multiply)
                {
                    unit.unit.statistics /= passive.GetStats();
                }
                else
                {
                    unit.unit.statistics -= passive.GetStats();
                }
            }
        }

        if (likelihoodPassives != null)
        {
            foreach (LikelihoodPassive likelihoodPassive in likelihoodPassives)
            {
                if (likelihoodPassive.multiply)
                {
                    unit.unit.statistics /= likelihoodPassive.GetStats();
                }
                else
                {
                    unit.unit.statistics -= likelihoodPassive.GetStats();
                }
                likelihoodPassive.DecreaseLikelihoodOfBeingTargeted();
            }
        }

        if (HPConditionals != null)
        {
            for (int i = 0; i < HPConditionals.Length; i++)
            {
                if (hasActivatedHP[i] && HPConditionals[i].multiply)
                {
                    unit.unit.statistics /= HPConditionals[i].GetStats();
                }

                else if (hasActivatedHP[i] && !HPConditionals[i].multiply)
                {
                    unit.unit.statistics -= HPConditionals[i].GetStats();
                }
                hasActivatedHP[i] = false;
            }
        }


        /*
        if (onHitConditionals != null)
        {
            for (int i = 0; i < onHitConditionals.Length; i++)
            {
                if (hasActivatedOnHit[i] && onHitConditionals[i].multiply)
                {
                    unit.unit.statistics /= onHitConditionals[i].GetStats();
                }
                else if (hasActivatedOnHit[i] && !onHitConditionals[i].multiply)
                {
                    unit.unit.statistics -= onHitConditionals[i].GetStats();
                }
                hasActivatedOnHit[i] = false;
            }
        }
        */
    }

    /*
    private IEnumerator OnHitActions(float randomNumber)
    {
        for (int i = 0; i < onHitConditionals.Length; i++) // didn't want to set the unit every frame
        {
            if (!hasActivatedOnHit[i] && onHitConditionals[i].CheckCondition(unit, randomNumber))
            {
                if (onHitConditionals[i].multiply)
                {
                    unit.unit.statistics *= onHitConditionals[i].GetStats();
                    yield return new WaitForSeconds(2);
                }
                else
                {
                    unit.unit.statistics += onHitConditionals[i].GetStats();
                    yield return new WaitForSeconds(2);
                }
                hasActivatedOnHit[i] = true;
            }
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < onHitConditionals.Length; i++)
        {
            if (hasActivatedOnHit[i])
            {
                if (onHitConditionals[i].multiply)
                {
                    unit.unit.statistics /= onHitConditionals[i].GetStats();
                }
                else
                {
                    unit.unit.statistics -= onHitConditionals[i].GetStats();
                }
                hasActivatedOnHit[i] = false;
            }
        }
        wasHit = false;
    }
    */

    #region Setters and Getters

    // setters and getters
    public void SetWasHit(bool wasHit)
    {
        Debug.Log("hit");
        this.wasHit = wasHit;
    }

    public bool GetWasHit()
    {
        return wasHit;
    }

    #endregion
}
