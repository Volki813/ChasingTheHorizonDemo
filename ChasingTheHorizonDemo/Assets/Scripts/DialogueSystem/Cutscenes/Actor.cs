using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public Image portrait { get; private set; }
    public bool isMoving { get; private set; }
    public string positionString { get; private set; }
    public RectTransform positionTransform { get; private set; }
    private Tween<Vector3> moveTween = null;
    
    public void CreateActor(string name, Image portrait, RectTransform positionTransform)
    {
        this.name = name;
        this.portrait = portrait;
        this.positionTransform = positionTransform;
    }

    public void MoveTo(Vector3 destinationPos, float timeToComplete)
    {
        isMoving = true;

        Vector3 startPos = positionTransform.anchoredPosition;
        moveTween = TweenManager.TweenAnchoredPosition(positionTransform, startPos, 
            destinationPos, timeToComplete).SetOnComplete(() => isMoving = false)
            .SetOnUpdate(CheckIfNextIsPressed);
    }

    public void CheckIfNextIsPressed()
    {
        if(DialogueManager.instance.nextIsPressed) moveTween.CompleteTween();
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

    public void SetPortrait(string portraitPath, string portraitName)
    {
        portrait.sprite = Resources.Load<Sprite>(portraitPath + name + "/" + portraitName);
        portrait.SetNativeSize();
        portrait.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void SetPositionString(string positionString)
    {
        this.positionString = positionString;
    }

    public IEnumerator Bounce()
    {
        yield return new WaitWhile(() => positionString == null);
        Vector3 startPos = positionTransform.anchoredPosition;
        Vector3 endPos = new Vector3(startPos.x, startPos.y + 30, startPos.z);
        TweenManager.TweenAnchoredPosition(positionTransform, startPos,
            endPos, 0.1f).SetPingPong(2).SetEase(EaseType.ExpoEaseOut);
    }
}
