using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ActorManager : MonoBehaviour
{
    public static ActorManager instance { get; private set; }
    public Dictionary<string, Actor> actorsDictionary = new Dictionary<string, Actor>();
    public Dictionary<string, List<Actor>> actorsPosition = new Dictionary<string, List<Actor>>(); // list of actors because one pos can hold more actors

    [SerializeField] private GameObject actorHolder = null;
    [SerializeField] private AnimatorController animatorController = null;
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
            Animator animator = actorObject.AddComponent<Animator>();

            actor.CreateActor(characterName, image, animator, animatorController);

            actorsDictionary[characterName] = actor;

            actorPositionObject.transform.SetParent(actorHolder.transform, false);
            actorObject.transform.SetParent(actorPositionObject.transform, false);

            return actor;
        }
    }

    public IEnumerator PlaceActorAtPosition(Actor actor, string position, Vector3 destination)
    {
        RemoveActorFromPreviousPositionList(actor);

        actor.gameObject.SetActive(false); // to prevent the one split second where the portrait spawns in the middle from being seen

        yield return null; // wait a frame because otherwise all actors will be placed on the same index 

        actor.gameObject.SetActive(true);

        AddActorToPositionList(actor, position);

        List<Actor> actorList = GetActorsAtPosition(position);

        int positionInList = GetPositionInList(actorList, actor);

        // arranges the images so the first one in the list has the highest priority
        actor.transform.parent.SetSiblingIndex(actorList.Count - 1 - positionInList);

        // place additional actors to the left if left, otherwise to the right
        Vector3 newPos = CalculateNewPos(position, positionInList, destination);

        actor.transform.parent.position = newPos;
    }

    public IEnumerator MoveActorToPosition(Actor actor, string position, Vector3 destination, float timeToComplete)
    {
        RemoveActorFromPreviousPositionList(actor);

        actor.gameObject.SetActive(false); // to prevent the one split second where the portrait spawns in the middle from being seen

        yield return null; // wait a frame because otherwise all actors will be placed on the same index 

        actor.gameObject.SetActive(true);

        AddActorToPositionList(actor, position);

        List<Actor> actorList = GetActorsAtPosition(position);

        int positionInList = GetPositionInList(actorList, actor);

        // arranges the images so the first one in the list has the highest priority
        actor.transform.parent.SetSiblingIndex(actorList.Count - 1 - positionInList);

        // place additional actors to the left if left, otherwise to the right
        Vector3 newPos = CalculateNewPos(position, positionInList, destination);

        StartCoroutine(actor.MoveTo(newPos, timeToComplete));
    }

    private Vector3 CalculateNewPos(string position, int positionInList, Vector3 destination)
    {
        Vector3 newPos = new Vector3();

        if (position == "far left" || position == "near left" || position == "center left")
            newPos = new Vector3(destination.x - (samePositionActorDistance * positionInList), destination.y, destination.z);
        else if (position == "far right" || position == "near right" || position == "center right")
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

    public bool AreActorsMoving()
    {
        bool actorsAreMoving = false;
        foreach (Actor actor in actorsDictionary.Values)
        {
            if (actor.isMoving)
            {
                actorsAreMoving = true;
            }
        }
        return actorsAreMoving;
    }

    private List<Actor> GetActorsAtPosition(string position)
    {
        List<Actor> actorsList = new List<Actor>();

        if (!actorsPosition.ContainsKey(position))
        {
            actorsPosition[position] = actorsList;
        }
        if (actorsPosition.TryGetValue(position, out List<Actor> actors))
        {
            actorsList = actors;
        }

        return actorsList;
    }

    private void RemoveActorFromPreviousPositionList(Actor actor)
    {
        if (actor.position == null) return; // return if actor has no position yet

        if (!actorsPosition.ContainsKey(actor.position)) // initialize new lists for key that don't exist yet
        {
            actorsPosition[actor.position] = new List<Actor>();
        }

        actorsPosition[actor.position].Remove(actor);
    }

    private void AddActorToPositionList(Actor actor, string position)
    {
        actor.SetPosition(position);

        if (!actorsPosition.ContainsKey(actor.position)) // initialize new lists for key that don't exist yet
        {
            actorsPosition[actor.position] = new List<Actor>();
        }

        actorsPosition[actor.position].Add(actor);
    }
}
