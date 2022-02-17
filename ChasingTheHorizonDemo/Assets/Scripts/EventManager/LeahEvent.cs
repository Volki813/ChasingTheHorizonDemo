using System;
using System.Collections;
using UnityEngine;

public class LeahEvent : Event
{
    private Camera mainCamera;
    private CursorController cursor;
    private LoseManager loseManager = null;
    public GameObject screenDim = null;
    public GameObject leahObject;
    public MapDialogue leahDialogue;

    private void Start()
    {
        cursor = FindObjectOfType<CursorController>();
        mainCamera = FindObjectOfType<Camera>();
        loseManager = FindObjectOfType<LoseManager>();
    }

    public override void StartEvent()
    {
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        loseManager.gameObject.SetActive(false);

        Vector3 targetPosition = new Vector3(leahObject.transform.position.x, leahObject.transform.position.y, -10);
        if(targetPosition.x > cursor.cameraRight){
            targetPosition.x = cursor.cameraRight;
        }
        if(targetPosition.x < cursor.cameraLeft){
            targetPosition.x = cursor.cameraLeft;
        }
        if(targetPosition.y > cursor.cameraTop){
            targetPosition.y = cursor.cameraTop;
        }
        if(targetPosition.y < cursor.cameraBottom){
            targetPosition.y = cursor.cameraBottom;
        }

        leahObject.SetActive(true);
        TurnManager.instance.allyUnits.Add(leahObject.GetComponent<UnitLoader>());        

        while(mainCamera.transform.position.x != targetPosition.x)
        {
            Vector3 xTarget = new Vector3(targetPosition.x, mainCamera.transform.position.y, -10);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, xTarget, 10f * Time.deltaTime);
            yield return null;
        }
        while(mainCamera.transform.position.y != targetPosition.y)
        {
            Vector3 yTarget = new Vector3(mainCamera.transform.position.x, targetPosition.y, -10);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, yTarget, 10f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitUntil(() => mainCamera.transform.position == targetPosition);

        StartCoroutine(MoveUnit(new Vector2(5.5f, -2.5f)));
        yield return new WaitUntil(() => (Vector2)leahObject.transform.position == new Vector2(5.5f, -2.5f));

        cursor.transform.position = new Vector3(leahObject.transform.position.x, leahObject.transform.position.y, 0);
        cursor.currentPosition = new Vector3(leahObject.transform.position.x, leahObject.transform.position.y, 0);

        MapDialogueManager.instance.WriteSingle(leahDialogue);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => !screenDim.activeSelf);
        loseManager.gameObject.SetActive(true);
        finished = true;
    }
    private IEnumerator MoveUnit(Vector2 targetPosition)
    {
        while(leahObject.transform.position.x != targetPosition.x)
        {
            if(leahObject.transform.position.x > targetPosition.x)
            {
                leahObject.GetComponent<Animator>().SetBool("Selected", true);
                leahObject.GetComponent<Animator>().SetBool("Left", true);
            }
            leahObject.transform.position = Vector2.MoveTowards(leahObject.transform.position, new Vector2(targetPosition.x, leahObject.transform.position.y), 2f * Time.deltaTime);
            yield return null;
        }
        leahObject.GetComponent<Animator>().SetBool("Selected", false);
        leahObject.GetComponent<Animator>().SetBool("Left", false);
    }
}
