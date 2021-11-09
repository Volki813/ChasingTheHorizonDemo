using UnityEngine;

//This script displays combat text prefab on the screen
//Used in conjuction with the Combat Manager
//This prefab destroys itself after half a second, this number can be tweaked if needed
public class BattleText : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend = null;

    public Sprite hit = null;
    public Sprite crit = null;
    public Sprite miss = null;


    private void Start()
    {
        Invoke("Destroy", 0.5f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }


    public void SetText(string text)
    {
        if(text == "Hit")
        {
            rend.sprite = hit;
        }
        else if(text == "Crit")
        {
            rend.sprite = crit;
        }
        else if(text == "Miss")
        {
            rend.sprite = miss;
        }
    }
}
