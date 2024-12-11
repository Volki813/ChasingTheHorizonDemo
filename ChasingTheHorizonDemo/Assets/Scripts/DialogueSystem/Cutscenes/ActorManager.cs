using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public List<Actor> actors = null;

    public Actor GetActorByName(string characterName) 
    {
        return actors.FirstOrDefault(a => a.name == characterName);
    }
}
