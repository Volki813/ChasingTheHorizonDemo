using UnityEngine;
using System.Collections;

//This script displays combat text prefab on the screen
//Used in conjuction with the Combat Manager
//This prefab destroys itself after half a second, this number can be tweaked if needed
public class BattleText : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend = null;

    private Animator animator;

    public Sprite hit = null;
    public Sprite crit = null;
    public Sprite miss = null;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.17f);
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
