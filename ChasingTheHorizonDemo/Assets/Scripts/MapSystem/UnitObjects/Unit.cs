using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "MapUnit")]
public class Unit : ScriptableObject
{
    public Sprite sprite;
    public string unitName;
    public bool allyUnit;

    public int health;
    public int strength;
    public int magic;
    public int defense;
    public int resistance;
    public int proficiency;
    public int motivation;
    public int agility;
    public int movement;

    public int level;
    public int exp;
}
