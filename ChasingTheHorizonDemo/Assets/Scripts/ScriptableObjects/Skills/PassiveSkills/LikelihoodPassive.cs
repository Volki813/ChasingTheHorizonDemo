using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive Skills/Likelihood Passive", fileName = "Likelihood Passive Skill")]
public class LikelihoodPassive : PassiveSkill
{
    [Range(0, 1)][SerializeField] private float likelihoodFactor;

    public void IncreaseLikelihoodOfBeingTargeted()
    {
        
    }

    public void DecreaseLikelihoodOfBeingTargeted()
    {

    }
}
