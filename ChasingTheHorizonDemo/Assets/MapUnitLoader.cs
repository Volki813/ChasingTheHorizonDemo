using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnitLoader : MonoBehaviour
{
    public GameObject normalModeUnits = null;
    public GameObject hardModeUnits = null;

    private void Awake()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        if(difficulty == "Normal")
        {
            normalModeUnits.SetActive(true);
        }
        else if(difficulty == "Hard")
        {
            hardModeUnits.SetActive(true);
        }
    }
}
