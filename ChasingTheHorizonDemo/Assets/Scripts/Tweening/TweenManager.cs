using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TweenManager : MonoBehaviour
{
    private static TweenManager instance;
    public static TweenManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject manager = new GameObject("TweenManager");
                instance = manager.AddComponent<TweenManager>();
            }
            return instance;
        }
    }

    /// <summary>
    /// List of active tweens.
    /// </summary>
    private Dictionary<string, ITween> activeTweens = new Dictionary<string, ITween>();

    private void Update()
    {
        foreach (var pair in activeTweens.ToList())
        {
            ITween tween = pair.Value;
            tween.Update();

            if (tween.IsComplete)
            {
                if(tween.onComplete != null)
                {
                    tween.onComplete.Invoke();
                    tween.onComplete = null;
                }

                RemoveTween(pair.Key);
            }

            if (tween.WasKilled)
            {
                RemoveTween(pair.Key);
            }
        }
    }

    /// <summary>
    /// Adds <paramref name="tween"/> to the active tweens dictionary. 
    /// Automatically called when a new Tween from the class Tween is created.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tween"></param>
    public void AddTween<T>(ITween tween)
    {
        // Kill the previous tween if another tween that accesses the same identifier is added. 
        if (activeTweens.ContainsKey(tween.Identifier))
        {
            activeTweens[tween.Identifier].onComplete?.Invoke();
            activeTweens[tween.Identifier].OnCompleteKill();
        }

        activeTweens[tween.Identifier] = tween;
    }

    /// <summary>
    /// Removes the tween with <paramref name="identifier"/> from the active tweens dictionary.
    /// </summary>
    /// <param name="identifier"></param>
    public void RemoveTween(string identifier)
    {
        activeTweens.Remove(identifier);
    }

    #region Helper Methods

    /// <summary>
    /// Moves a <paramref name="gameObject"/> from <paramref name="startPosition"/> to <paramref name="endPosition"/> in <paramref name="duration"/> seconds.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="startPosition"></param>
    /// <param name="endPosition"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tween<Vector3> TweenPosition(GameObject gameObject, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        Transform transform = gameObject.transform.GetComponent<Transform>();
        string identifier = $"{transform.GetInstanceID()}_Position";

        Tween<Vector3> tween = new Tween<Vector3>(gameObject, identifier, startPosition, endPosition, duration, value =>
        {
            transform.position = value;
        });

        return tween;
    }

    public static Tween<Vector3> TweenPosition(Transform transform, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        string identifier = $"{transform.GetInstanceID()}_Position";

        Tween<Vector3> tween = new Tween<Vector3>(transform, identifier, startPosition, endPosition, duration, value =>
        {
            transform.position = value;
        });

        return tween;
    }

    /// <summary>
    /// Moves a UI <paramref name="gameObject"/> from <paramref name="startPosition"/> to <paramref name="endPosition"/> in <paramref name="duration"/> seconds.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="startPosition"></param>
    /// <param name="endPosition"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tween<Vector3> TweenAnchoredPosition(GameObject gameObject, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        RectTransform rectTransform = gameObject.transform.GetComponent<RectTransform>();
        string identifier = $"{rectTransform.GetInstanceID()}_Anchored_Position";

        Tween<Vector3> tween = new Tween<Vector3>(gameObject, identifier, startPosition, endPosition, duration, value =>
        {
            rectTransform.anchoredPosition = value;
        });

        return tween;
    }

    public static Tween<Vector3> TweenAnchoredPosition(RectTransform rectTransform, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        string identifier = $"{rectTransform.GetInstanceID()}_Anchored_Position";

        Tween<Vector3> tween = new Tween<Vector3>(rectTransform, identifier, startPosition, endPosition, duration, value =>
        {
            rectTransform.anchoredPosition = value;
        });

        return tween;
    }

    /// <summary>
    /// Moves a UI <paramref name="gameObject"/>'s y value from <paramref name="startPosition"/> to <paramref name="endPosition"/> in <paramref name="duration"/> seconds.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="startPosition"></param>
    /// <param name="endPosition"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tween<float> TweenAnchoredPositionY(GameObject gameObject, float startPosition, float endPosition, float duration)
    {
        RectTransform rectTransform = gameObject.transform.GetComponent<RectTransform>();
        string identifier = $"{rectTransform.GetInstanceID()}_Anchored_Position_Y";

        Tween<float> tween = new Tween<float>(gameObject, identifier, startPosition, endPosition, duration, value =>
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, value);
        });

        return tween;
    }

    public static Tween<float> TweenAnchoredPositionY(RectTransform rectTransform, float startPosition, float endPosition, float duration)
    {
        string identifier = $"{rectTransform.GetInstanceID()}_Anchored_Position_Y";

        Tween<float> tween = new Tween<float>(rectTransform, identifier, startPosition, endPosition, duration, value =>
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, value);
        });

        return tween;
    }

    /// <summary>
    /// Moves a UI <paramref name="gameObject"/>'s x value from <paramref name="startPosition"/> to <paramref name="endPosition"/> in <paramref name="duration"/> seconds.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="startPosition"></param>
    /// <param name="endPosition"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tween<float> TweenAnchoredPositionX(GameObject gameObject, float startPosition, float endPosition, float duration)
    {
        RectTransform rectTransform = gameObject.transform.GetComponent<RectTransform>();
        string identifier = $"{rectTransform.GetInstanceID()}_Anchored_Position_X";

        Tween<float> tween = new Tween<float>(gameObject, identifier, startPosition, endPosition, duration, value =>
        {
            rectTransform.anchoredPosition = new Vector2(value, rectTransform.anchoredPosition.y);
        });

        return tween;
    }

    public static Tween<float> TweenAnchoredPositionX(RectTransform rectTransform, float startPosition, float endPosition, float duration)
    {
        string identifier = $"{rectTransform.GetInstanceID()}_Anchored_Position_X";

        Tween<float> tween = new Tween<float>(rectTransform, identifier, startPosition, endPosition, duration, value =>
        {
            rectTransform.anchoredPosition = new Vector2(value, rectTransform.anchoredPosition.y);
        });

        return tween;
    }

    /// <summary>
    /// Interpolates <paramref name="startAlpha"/> of a sprite to <paramref name="endAlpha"/> in <paramref name="duration"/> seconds.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="startAlpha"></param>
    /// <param name="endAlpha"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tween<float> TweenSpriteAlpha(GameObject gameObject, float startAlpha, float endAlpha, float duration)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        string identifier = $"{spriteRenderer.GetInstanceID()}_Alpha";

        Tween<float> tween = new Tween<float>(gameObject, identifier, startAlpha, endAlpha, duration, value =>
            {
                Color color = spriteRenderer.color;
                color.a = value;
                spriteRenderer.color = color;
            });

        return tween;
    }

    public static Tween<float> TweenSpriteAlpha(SpriteRenderer spriteRenderer, float startAlpha, float endAlpha, float duration)
    {
        string identifier = $"{spriteRenderer.GetInstanceID()}_Alpha";

        Tween<float> tween = new Tween<float>(spriteRenderer, identifier, startAlpha, endAlpha, duration, value =>
        {
            Color color = spriteRenderer.color;
            color.a = value;
            spriteRenderer.color = color;
        });

        return tween;
    }

    /// <summary>
    /// Scales a <paramref name="gameObject"/> from <paramref name="startScale"/> to <paramref name="endScale"/> in <paramref name="duration"/> seconds.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="startScale"></param>
    /// <param name="endScale"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tween<Vector3> TweenScale(GameObject gameObject, Vector3 startScale, Vector3 endScale, float duration)
    {
        Transform transform = gameObject.transform.GetComponent<Transform>();
        string identifier = $"{transform.GetInstanceID()}_Scale";

        Tween<Vector3> tween = new Tween<Vector3>(gameObject, identifier, startScale, endScale, duration, value =>
        {
            transform.localScale = value;
        });

        return tween;
    }

    public static Tween<Vector3> TweenScale(Transform transform, Vector3 startScale, Vector3 endScale, float duration)
    {
        string identifier = $"{transform.GetInstanceID()}_Scale";

        Tween<Vector3> tween = new Tween<Vector3>(transform, identifier, startScale, endScale, duration, value =>
        {
            transform.localScale = value;
        });

        return tween;
    }

    #endregion
}
