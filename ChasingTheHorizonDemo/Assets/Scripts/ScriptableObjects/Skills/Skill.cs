using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public enum SkillType
    {
        ACTIVE,
        CONDITIONAL,
        PASSIVE,
        DEFAULT // just to be able to assign skilltype in this script
    }

    [Header("Skill Attributes")]
    public string skillName = null;
    public Sprite skillIcon = null;
    public string skillDescription = null;
    public SkillType skillType = SkillType.DEFAULT;
}
