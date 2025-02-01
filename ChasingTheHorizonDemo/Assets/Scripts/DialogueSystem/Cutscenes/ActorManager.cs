using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ActorManager : MonoBehaviour
{
    public static ActorManager instance;
    public Dictionary<string, Actor> actors = new Dictionary<string, Actor>();

    [SerializeField] private GameObject actorHolder = null;
    [SerializeField] private AnimatorController animatorController = null;

    public void Awake()
    {
        if (instance == null) instance = this;
    }

    public Actor GetActorByName(string characterName)
    {
        if (actors.ContainsKey(characterName))
        {
            return actors[characterName];
        }
        else
        {
            GameObject actorPositionObject = new GameObject(characterName + " position");
            GameObject actorObject = new GameObject(characterName);

            Actor actor = actorObject.AddComponent<Actor>();
            actor.CreateActor(characterName, actorObject.AddComponent<Image>(), 
                actorObject.AddComponent<Animator>(), animatorController);

            actors[characterName] = actor;

            actorPositionObject.transform.SetParent(actorHolder.transform, false);
            actorObject.transform.SetParent(actorPositionObject.transform, false);

            return actor;
        }
    }
}
