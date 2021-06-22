using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class Chapter0Events : MonoBehaviour
{
    public bool leahSpawned;

    public GameObject leah;
    DialogueHolder dialogueHolder;

    public GameObject leahDialogue;
    public GameObject royAttacked;
    public GameObject royDefeated;

    private void Start()
    {
        dialogueHolder = FindObjectOfType<DialogueHolder>();
    }

    private void Update()
    {
        if(leahSpawned == false)
        {
            LeahSpawn();
        }
    }

    private void LeahSpawn()
    {
        if(TurnManager.instance.turnNumber == 2)
        {
            leah.SetActive(true);
            TurnManager.instance.UpdateTiles();
            TurnManager.instance.allyUnits.Add(leah.GetComponent<UnitLoader>());
            leahSpawned = true;
            leahDialogue.transform.SetParent(dialogueHolder.transform);
            dialogueHolder.StartDialogue();
        }
    }

    private void RoyAttacked()
    {

    }

    private void RoyDefeated()
    {

    }
}
