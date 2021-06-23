using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "MapUnit")]
public class Unit : ScriptableObject
{
    public Sprite sprite;
    public Sprite portrait;

    public string unitName;
    public bool allyUnit;

    //Put the statistics
    public Statistics statistics;

    //wont change yet but these should be on the monobehaviour
    //Scriptable objects aren't made to be changed during gameplay but define a data set to be loaded onto blank objects
    public int level;
    public int exp;
}
