using UnityEngine;

//BehaviorTags attach to enemy units and determine their AI properties
//Used in conjuction with the AI Manager
//Make sure every enemy unit in a given scene has a BehaviorTag, a behavior bool checked, and a number for their order
public class BehaviorTag : MonoBehaviour
{
    //VARIABLES
    public int order;
    public bool blitz;
    public bool defensive;
    public bool boss;
}
