using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive Skills/Likelihood Passive", fileName = "Likelihood Passive Skill")]
public class LikelihoodPassive : PassiveSkill
{
    [SerializeField] private float likelihoodFactor;

    public void IncreaseLikelihoodOfBeingTargeted()
    {
        // increase value that influences likelihood of being targeted when implented    
    }

    public void DecreaseLikelihoodOfBeingTargeted()
    {
        // decrease value that influences likelihood of being targeted when implented  
    }
}
