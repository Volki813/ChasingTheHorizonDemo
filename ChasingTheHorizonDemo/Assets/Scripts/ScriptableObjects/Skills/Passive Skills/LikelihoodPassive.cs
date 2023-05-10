using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive Skills/Likelihood Passive", fileName = "Likelihood Passive Skill")]
public class LikelihoodPassive : PassiveSkill
{
    [SerializeField] private float likelihoodFactor;

    public float GetLikelihood()
    {
        return likelihoodFactor;
    }
}
