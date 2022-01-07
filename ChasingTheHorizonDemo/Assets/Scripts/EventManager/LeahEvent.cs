using System;
using System.Collections;
using UnityEngine;

public class LeahEvent : Event
{
    private Camera mainCamera;
    private CursorController cursor;
    public GameObject leahObject;
    public MapDialogue leahDialogue;

    private void Start()
    {
        cursor = FindObjectOfType<CursorController>();
        mainCamera = FindObjectOfType<Camera>();
    }

    public override void StartEvent()
    {
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        Vector3 targetPosition = new Vector3(leahObject.transform.position.x, leahObject.transform.position.y, -10);

        leahObject.SetActive(true);
        TurnManager.instance.allyUnits.Add(leahObject.GetComponent<UnitLoader>());
        
        cursor.transform.position = new Vector3(leahObject.transform.position.x, leahObject.transform.position.y, 0);
        cursor.currentPosition = new Vector3(leahObject.transform.position.x, leahObject.transform.position.y, 0);

        while(mainCamera.transform.position.x != targetPosition.x)
        {
            Vector3 xTarget = new Vector3(targetPosition.x, mainCamera.transform.position.y, -10);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, xTarget, 3f * Time.fixedDeltaTime);
            yield return null;
        }
        while(mainCamera.transform.position.y != targetPosition.y)
        {
            Vector3 yTarget = new Vector3(mainCamera.transform.position.x, targetPosition.y, -10);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, yTarget, 3f * Time.fixedDeltaTime);
            yield return null;
        }
        yield return new WaitUntil(() => mainCamera.transform.position == targetPosition);
        yield return new WaitForSeconds(1f);
        MapDialogueManager.instance.WriteSingle(leahDialogue);
        finished = true;
    }
}
