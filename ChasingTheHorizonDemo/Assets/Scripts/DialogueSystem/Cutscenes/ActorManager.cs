using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ActorManager : MonoBehaviour
{
    [HideInInspector] public List<Actor> actors = null;
    [SerializeField] private GameObject actorHolder = null;
    [SerializeField] private AnimatorController animatorController = null;

    public Actor GetActorByName(string characterName) 
    {
        Actor actor = actors.FirstOrDefault(a => a.name == characterName);
        
        if(actor == null)
        {
            actor = new Actor();
            
            GameObject characterObject = new GameObject(characterName);
            characterObject.AddComponent<Image>();
            characterObject.AddComponent<Animator>();
            
            actor.name = characterName;
            actor.position = characterObject.transform;
            actor.portrait = characterObject.GetComponent<Image>();
            actor.animator = characterObject.GetComponent<Animator>();

            actor.animator.runtimeAnimatorController = animatorController;

            actors.Add(actor);

            characterObject.transform.SetParent(actorHolder.transform, false);
        }

        return actor;
    }
}
