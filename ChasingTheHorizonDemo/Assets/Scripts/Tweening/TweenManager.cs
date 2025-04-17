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
        if (activeTweens.ContainsKey(tween.Identifier)) activeTweens[tween.Identifier].OnCompleteKill();

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
    /// Interpolates <paramref name="startAlpha"/> of a sprite to <paramref name="endAlpha"/>.
    /// </summary>
    /// <param name="gameObject"> The game object that contains the sprite. </param>
    /// <param name="startAlpha"> The alpha value at which the tween starts. </param>
    /// <param name="endAlpha"> The alpha value at which the tween ends. </param>
    /// <param name="duration"> Duration in seconds. </param>
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

    /// <summary>
    /// Interpolates <paramref name="startScale"/> of <paramref name="gameObject"/> to <paramref name="endScale"/>.
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

    #endregion
}
