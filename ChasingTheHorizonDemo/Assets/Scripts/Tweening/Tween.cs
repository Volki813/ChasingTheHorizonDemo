using System;
using UnityEngine;

public class Tween<T> : ITween
{
    private T startValue;
    private T endValue;
    private float duration;
    private Action<T> onTweenUpdate;
    private float elapsedTime;

    public Tween(object target, string identifier, T startValue, T endValue, float duration, Action<T> onTweenUpdate)
    {
        Target = target;
        Identifier = identifier;
        this.startValue = startValue;
        this.endValue = endValue;
        this.duration = duration;
        this.onTweenUpdate = onTweenUpdate;

        TweenManager.Instance.AddTween<T>(this);
    }

    public object Target { get; private set; }

    public bool IsComplete { get; private set; }

    public bool WasKilled { get; private set; }

    public bool IsPaused { get; private set; }

    public bool IgnoreTimeScale { get; private set; }

    public string Identifier { get; private set; }

    public float DelayTime { get; private set; }

    public Action onComplete { get; set; }

    public void Update()
    {
        if (IsComplete) return;

        elapsedTime += Time.deltaTime;
        float t = elapsedTime / duration;
        T currentValue;

        currentValue = Interpolate(startValue, endValue, t);
        onTweenUpdate?.Invoke(currentValue);

        if (elapsedTime >= duration)
        {
            IsComplete = true;
        }
    }

    /// <summary>
    /// Lerps the start value to the end value within a sepcific duration. 
    /// Will throw and exception if the given type hasn't been implemented.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public T Interpolate(T start, T end, float t)
    {
        // Types to Tween can be expanded here

        if (start is float startFloat && end is float endFloat)
            return (T)(object)Mathf.LerpUnclamped(startFloat, endFloat, t);

        if (start is Vector3 startVector3 && end is Vector3 endVector3)
            return (T)(object)Vector3.LerpUnclamped(startVector3, endVector3, t);

        if (start is Vector2 startVector2 && end is Vector2 endVector2)
            return (T)(object)Vector2.LerpUnclamped(startVector2, endVector2, t);

        if (start is Color startColor && end is Color endColor)
            return (T)(object)Color.Lerp(startColor, endColor, t);

        throw new NotImplementedException($"Interpolation for type {typeof(T)} not implemented.");
    }

    public bool IsTargetDestroyed()
    {
        return false;
    }

    public void OnCompleteKill()
    {

    }

    public void FullKill()
    {

    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }

    #region Ease Calculations

    public static float Linear(float t)
    {
        return t;
    }

    public static float ExpoEaseIn(float t)
    {
        return (t == 0) ? 0 : Mathf.Pow(2, 10 * t - 10);
    }

    public static float ExpoEaseOut(float t)
    {
        return (t == 0) ? 1 : 1 - Mathf.Pow(2, -10 * t);
    }

    public static float ExpoEaseInOut(float t)
    {
        return (t == 0) ? 0 : (t == 1) ? 1 : (t < 0.5f) ? Mathf.Pow(2, 20 * t - 10) * 0.5f : 1 - Mathf.Pow(2, -20 * t + 10) * 0.5f;
    }

    public static float ExpoEaseOutIn(float t)
    {
        return (t < 0.5f) ? ExpoEaseOut(2 * t) * 0.5f : ExpoEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    public static float CircEaseIn(float t)
    {
        return 1 - Mathf.Sqrt(1 - t * t);
    }

    public static float CircEaseOut(float t)
    {
        return Mathf.Sqrt(1 - (t - 1) * (t - 1));
    }

    public static float CircEaseInOut(float t)
    {
        return (t < 0.5f) ? (1 - Mathf.Sqrt(1 - 4 * t * t)) * 0.5f : (Mathf.Sqrt(1 - (-2 * t + 2) * (-2 * t + 2)) + 1) * 0.5f;
    }

    public static float CircEaseOutIn(float t)
    {
        return (t < 0.5f) ? CircEaseOut(2 * t) * 0.5f : CircEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    public static float QuadEaseIn(float t)
    {
        return t * t;
    }

    public static float QuadEaseOut(float t)
    {
        return t * (2 - t);
    }

    public static float QuadEaseInOut(float t)
    {
        return (t < 0.5f) ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }

    public static float QuadEaseOutIn(float t)
    {
        return (t < 0.5f) ? QuadEaseOut(2 * t) * 0.5f : QuadEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    public static float SineEaseIn(float t)
    {
        return 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
    }

    public static float SineEaseOut(float t)
    {
        return Mathf.Sin(t * Mathf.PI * 0.5f);
    }

    public static float SineEaseInOut(float t)
    {
        return -0.5f * (Mathf.Cos(Mathf.PI * t) - 1);
    }

    public static float SineEaseOutIn(float t)
    {
        return (t < 0.5f) ? SineEaseOut(2 * t) * 0.5f : SineEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    public static float CubicEaseIn(float t)
    {
        return t * t * t;
    }

    public static float CubicEaseOut(float t)
    {
        return 1 - (1 - t) * (1 - t) * (1 - t);
    }

    public static float CubicEaseInOut(float t)
    {
        return (t < 0.5) ? 4 * t * t * t : 1 - (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * 0.5f;
    }

    public static float CubicEaseOutIn(float t)
    {
        return (t < 0.5f) ? CubicEaseOut(2 * t) * 0.5f : CubicEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    public static float QuartEaseIn(float t)
    {
        return t * t * t * t;
    }

    public static float QuartEaseOut(float t)
    {
        return 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t);
    }

    public static float QuartEaseInOut(float t)
    {
        return (t < 0.5f) ? 8 * t * t * t * t : 1 - (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * 0.5f;
    }

    public static float QuartEaseOutIn(float t)
    {
        return (t < 0.5f) ? QuartEaseOut(2 * t) * 0.5f : QuartEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    public static float QuintEaseIn(float t)
    {
        return t * t * t * t * t;
    }

    public static float QuintEaseOut(float t)
    {
        return 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t) * (1 - t);
    }

    public static float QuintEaseInOut(float t)
    {
        return (t < 0.5f) ? 16 * t * t * t * t * t : 1 - (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * 0.5f;
    }

    public static float QuintEaseOutIn(float t)
    {
        return (t < 0.5f) ? QuintEaseOut(2 * t) * 0.5f : QuintEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    public static float ElasticEaseIn(float t)
    {
        return (t == 0) ? 0 : (t == 1) ? 1 : -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * (2 * Mathf.PI) / 3);
    }

    public static float ElasticEaseOut(float t)
    {
        return (t == 0) ? 0 : (t == 1) ? 1 : Mathf.Pow(2, -10 * t) * Mathf.Sin(t * 10 - 0.75f) * (2 * Mathf.PI / 3) + 1;
    }

    public static float ElasticEaseInOut(float t)
    {
        return (t == 0) ? 0 : (t == 1) ? 1 : (t < 0.5f) ?
            -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * (2 * Mathf.PI / 4.5f))) * 0.5f :
            Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * (2 * Mathf.PI / 4.5f)) * 0.5f + 1;
    }

    public static float ElasticEaseOutIn(float t)
    {
        return (t < 0.5f) ? ElasticEaseOut(2 * t) * 0.5f : ElasticEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    public static float BounceEaseIn(float t)
    {
        return 1 - BounceEaseOut(1 - t);
    }

    public static float BounceEaseOut(float t)
    {
        if (t < 1 / 2.75f) return 7.5625f * t * t;
        else if (t < 2 / 2.75f) return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
        else if (t < 2.5f / 2.75f) return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
        else return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
    }

    public static float BounceEaseInOut(float t)
    {
        return (t < 0.5f) ? BounceEaseIn(t * 2) * 0.5f : BounceEaseOut(t * 2 - 1) * 0.5f + 0.5f;
    }

    public static float BounceEaseOutIn(float t)
    {
        return (t < 0.5f) ? BounceEaseOut(2 * t) * 0.5f : BounceEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    public static float BackEaseIn(float t)
    {
        float s = 1.70158f;
        return t * t * ((s + 1) * t - s);
    }

    public static float BackEaseOut(float t)
    {
        float s = 1.70158f;
        return (t -= 1) * t * ((s + 1) * t + s) + 1;
    }

    public static float BackEaseInOut(float t)
    {
        float s = 1.70258f * 1.525f;
        return (t < 0.5f) ? (t * 2) * (t * 2) * ((s + 1) * (t * 2) - s) * 0.5f :
            ((t * 2 - 2) * (t * 2 - 2) * ((s + 1) * (t * 2 - 2) + s) + 2) * 0.5f;
    }

    public static float BackEaseOutIn(float t)
    {
        return (t < 0.5f) ? BackEaseOut(2 * t) * 0.5f : BackEaseIn(2 * t - 1) * 0.5f + 0.5f;
    }

    #endregion
}

#region Ease Types

public enum EaseType
{
    Linear,
    ExpoEaseIn,
    ExpoEaseOut,
    ExpoEaseInOut,
    ExpoEaseOutIn,
    CircEaseIn,
    CircEaseOut,
    CircEaseInOut,
    CircEaseOutIn,
    QuadEaseIn,
    QuadEaseOut,
    QuadEaseInOut,
    QuadEaseOutIn,
    SineEaseIn,
    SineEaseOut,
    SineEaseInOut,
    SineEaseOutIn,
    CubicEaseIn,
    CubicEaseOut,
    CubicEaseInOut,
    CubicEaseOutIn,
    QuartEaseIn,
    QuartEaseOut,
    QuartEaseInOut,
    QuartEaseOutIn,
    QuintEaseIn,
    QuintEaseOut,
    QuintEaseInOut,
    QuintEaseOutIn,
    ElasticEaseIn,
    ElasticEaseOut,
    ElasticEaseInOut,
    ElasticEaseOutIn,
    BounceEaseIn,
    BounceEaseOut,
    BounceEaseInOut,
    BounceEaseOutIn,
    BackEaseIn,
    BackEaseOut,
    BackEaseInOut,
    BackEaseOutIn
}

#endregion