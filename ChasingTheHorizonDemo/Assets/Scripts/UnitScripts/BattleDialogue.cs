﻿using UnityEngine;

public class BattleDialogue : MonoBehaviour
{
    public MapDialogue[] criticalQuotes = null;
    public MapDialogue under50Quote = null;
    public MapDialogue deathQuote = null;
    public MapDialogue RandomCritQuote()
    {
        int randomNumber = Random.Range(0, criticalQuotes.Length);
        return criticalQuotes[randomNumber];
    }
}
