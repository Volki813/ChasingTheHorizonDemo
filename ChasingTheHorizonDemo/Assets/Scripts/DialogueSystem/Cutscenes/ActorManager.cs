using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActorManager : MonoBehaviour
{
    public static ActorManager instance { get; private set; }
    public Dictionary<string, Actor> actorsDictionary = new Dictionary<string, Actor>();
    public Dictionary<string, List<Actor>> actorsPosition = new Dictionary<string, List<Actor>>(); // list of actors because one pos can hold more actors

    [SerializeField] private GameObject actorHolder = null;
    [SerializeField] private int samePositionActorDistance = 50;

    public void Awake()
    {
        if (instance == null) instance = this;
    }

    public Actor GetActorByName(string characterName)
    {
        if (actorsDictionary.ContainsKey(characterName))
        {
            return actorsDictionary[characterName];
        }
        else
        {
            GameObject actorPositionObject = new GameObject(characterName + " position");
            GameObject actorObject = new GameObject(characterName);

            Actor actor = actorObject.AddComponent<Actor>();
            Image image = actorObject.AddComponent<Image>();
            RectTransform rectTransform = actorPositionObject.AddComponent<RectTransform>();

            actor.CreateActor(characterName, image, rectTransform);

            actorsDictionary[characterName] = actor;

            actorPositionObject.transform.SetParent(actorHolder.transform, false);
            actorObject.transform.SetParent(actorPositionObject.transform, false);

            return actor;
        }
    }

    public IEnumerator PlaceActorAtPosition(Actor actor, string positionString, Vector3 destination)
    {
        RemoveActorFromPreviousPositionList(actor);

        actor.gameObject.SetActive(false); // to prevent the one split second where the portrait spawns in the middle from being seen

        yield return null; // wait a frame because otherwise all actors will be placed on the same index 

        actor.gameObject.SetActive(true);

        AddActorToPositionList(actor, positionString);

        List<Actor> actorList = GetActorsAtPosition(positionString);

        int positionInList = GetPositionInList(actorList, actor);

        // arranges the images so the first one in the list has the highest priority
        actor.positionTransform.SetSiblingIndex(actorList.Count - 1 - positionInList);

        // place additional actors to the left if left, otherwise to the right
        Vector3 newPos = CalculateNewPos(positionString, positionInList, destination);

        actor.positionTransform.anchoredPosition = newPos;
    }

    public IEnumerator MoveActorToPosition(Actor actor, string positionString, Vector3 destination, float timeToComplete)
    {
        RemoveActorFromPreviousPositionList(actor);

        actor.gameObject.SetActive(false); // to prevent the one split second where the portrait spawns in the middle from being seen

        yield return null; // wait a frame because otherwise all actors will be placed on the same index 

        actor.gameObject.SetActive(true);

        AddActorToPositionList(actor, positionString);

        List<Actor> actorList = GetActorsAtPosition(positionString);

        int positionInList = GetPositionInList(actorList, actor);

        // arranges the images so the first one in the list has the highest priority
        actor.transform.parent.SetSiblingIndex(actorList.Count - 1 - positionInList);

        // place additional actors to the left if left, otherwise to the right
        Vector3 newPos = CalculateNewPos(positionString, positionInList, destination);

        StartCoroutine(actor.MoveTo(newPos, timeToComplete));
    }

    private Vector3 CalculateNewPos(string positionString, int positionInList, Vector3 destination)
    {
        Vector3 newPos = new Vector3();

        if (positionString == "far left" || positionString == "near left" || positionString == "center left")
            newPos = new Vector3(destination.x - (samePositionActorDistance * positionInList), destination.y, destination.z);
        else if (positionString == "far right" || positionString == "near right" || positionString == "center right")
            newPos = new Vector3(destination.x + (samePositionActorDistance * positionInList), destination.y, destination.z);
        // there's probably a smarter way to do this but I'm tired boss

        return newPos;
    }

    private int GetPositionInList(List<Actor> actorList, Actor actor)
    {
        int positionInList = 0;

        foreach (Actor actorInList in actorList)
        {
            if (actorInList == actor)
            {
                positionInList = actorList.IndexOf(actor);
            }
        }
        return positionInList;
    }

    public bool AreActorsNotMoving()
    {
        // checks if isMoving is false for all actors
        bool actorsAreNotMoving = actorsDictionary.Values.All(actor => !actor.isMoving);
        return actorsAreNotMoving;
    }

    private List<Actor> GetActorsAtPosition(string positionString)
    {
        List<Actor> actorsList = new List<Actor>();

        if (!actorsPosition.ContainsKey(positionString))
        {
            actorsPosition[positionString] = actorsList;
        }
        if (actorsPosition.TryGetValue(positionString, out List<Actor> actors))
        {
            actorsList = actors;
        }

        return actorsList;
    }

    private void RemoveActorFromPreviousPositionList(Actor actor)
    {
        if (actor.positionString == null) return; // return if actor has no position yet

        if (!actorsPosition.ContainsKey(actor.positionString)) // initialize new lists for key that don't exist yet
        {
            actorsPosition[actor.positionString] = new List<Actor>();
        }

        actorsPosition[actor.positionString].Remove(actor);
    }

    private void AddActorToPositionList(Actor actor, string positionString)
    {
        actor.SetPositionString(positionString);

        if (!actorsPosition.ContainsKey(actor.positionString)) // initialize new lists for key that don't exist yet
        {
            actorsPosition[actor.positionString] = new List<Actor>();
        }

        actorsPosition[actor.positionString].Add(actor);
    }
}
