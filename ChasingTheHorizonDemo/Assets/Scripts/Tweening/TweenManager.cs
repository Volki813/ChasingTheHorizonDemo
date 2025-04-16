using System.Collections.Generic;
using UnityEngine;

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
    /// List of active tweens
    /// </summary>
    private Dictionary<string, ITween> activeTweens = new Dictionary<string, ITween>();

    private void Update()
    {
        foreach (var pair in activeTweens)
        {
            ITween tween = pair.Value;
            tween.Update();
        }
    }

    /// <summary>
    /// Adds a new tween to the active tweens dictionary; automatically called when a new Tween from the class Tween
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tween"></param>
    public void AddTween<T>(ITween tween)
    {
        activeTweens[tween.Identifier] = tween;
    }

    #region Helper Methods

    /// <summary>
    /// Interpolates the start alpha value of a sprite to the end alpha value
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

    #endregion
}
