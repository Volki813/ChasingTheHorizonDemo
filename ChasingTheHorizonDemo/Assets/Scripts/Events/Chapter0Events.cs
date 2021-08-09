using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class Chapter0Events : MonoBehaviour
{
    private CursorControls controls;

    public bool leahSpawned;

    public GameObject leah;
    DialogueHolder dialogueHolder;

    public GameObject leahDialogue;

    private void Awake()
    {
        controls = new CursorControls();
    }

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
            controls.NeutralCursor.Enable();
            leah.SetActive(true);
            TurnManager.instance.UpdateTiles();
            TurnManager.instance.allyUnits.Add(leah.GetComponent<UnitLoader>());
            leahSpawned = true;
            leahDialogue.transform.SetParent(dialogueHolder.transform);
            dialogueHolder.StartDialogue();
        }
    }
}
