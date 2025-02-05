using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public Image portrait { get; private set; }
    public Animator animator { get; private set; }
    public bool isMoving { get; private set; }
    public string position { get; private set; }
    
    public void CreateActor(string name, Image portrait, Animator animator, RuntimeAnimatorController animatorController)
    {
        this.name = name;
        this.portrait = portrait;
        this.animator = animator;
        animator.runtimeAnimatorController = animatorController;
    }

    public IEnumerator MoveTo(Vector3 destinationPos, float timeToComplete)
    {
        isMoving = true;

        Vector3 startPos = transform.parent.position;
        float timeElapsed = 0f;
        while (timeElapsed <= timeToComplete)
        {
            if (DialogueManager.instance.nextIsPressed) // immediately puts actor to target position
            {
                transform.parent.position = destinationPos;
                break;
            }
            transform.parent.position =
                Vector3.Lerp(startPos, destinationPos, timeElapsed / timeToComplete);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.parent.position = destinationPos;

        isMoving = false;
    }

    public void SetFacingDirection(string direction)
    {
        Vector3 scale = portrait.transform.localScale;
        switch (direction) // direction is either "left" or "right"
        {
            case "left":
                if (Math.Sign(portrait.transform.localScale.x) > 0)
                    portrait.transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
                break;
            case "right":
                if (Math.Sign(portrait.transform.localScale.x) < 0) // negative sign of localscale.x means they're facing left
                    portrait.transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
                break;
        }
    }

    public void SetPortrait(string portraitPath, string portraitName, Actor actor)
    {
        actor.portrait.sprite = Resources.Load<Sprite>(portraitPath + actor.name + "/" + portraitName);
        actor.portrait.SetNativeSize();
        actor.portrait.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void SetPosition(string position)
    {
        this.position = position;
    }
    public void SetIsMoving(bool isMoving)
    {
        this.isMoving = isMoving;
    }
}
