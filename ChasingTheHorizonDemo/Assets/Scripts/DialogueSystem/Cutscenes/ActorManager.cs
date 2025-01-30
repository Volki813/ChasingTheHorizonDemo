using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActorManager : MonoBehaviour
{
    public List<Actor> actors = null;
    [SerializeField] private GameObject actorHolder = null;

    public Actor GetActorByName(string characterName) 
    {
        Actor actor = actors.FirstOrDefault(a => a.name == characterName);
        
        if(actor == null)
        {
            actor = new Actor();
            
            GameObject characterObject = new GameObject(characterName);
            characterObject.AddComponent<Image>();
            
            actor.name = characterName;
            actor.position = characterObject.transform;
            actor.portrait = characterObject.GetComponent<Image>();
            actors.Add(actor);

            characterObject.transform.SetParent(actorHolder.transform, false);
        }

        return actor;
    }
}
