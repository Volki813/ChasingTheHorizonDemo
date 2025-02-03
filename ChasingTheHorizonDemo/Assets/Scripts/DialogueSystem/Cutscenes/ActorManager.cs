using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ActorManager : MonoBehaviour
{
    public static ActorManager instance { get; private set; }
    public Dictionary<string, Actor> actorsDictionary = new Dictionary<string, Actor>();

    [SerializeField] private GameObject actorHolder = null;
    [SerializeField] private AnimatorController animatorController = null;

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
            actor.CreateActor(characterName, actorObject.AddComponent<Image>(),
                actorObject.AddComponent<Animator>(), animatorController);

            actorsDictionary[characterName] = actor;

            actorPositionObject.transform.SetParent(actorHolder.transform, false);
            actorObject.transform.SetParent(actorPositionObject.transform, false);

            return actor;
        }
    }

    public bool AreActorsMoving()
    {
        bool actorsAreMoving = false;
        foreach (Actor actor in actorsDictionary.Values)
        {
            if (actor.isMoving)
            {
                actorsAreMoving = actor.isMoving;
            }
        }
        // to prevent the next line from typing immediately if characters are moving and line is finished typing
        DialogueManager.instance.nextIsPressed = false;  
        return actorsAreMoving;
    }
}
