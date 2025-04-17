using System;
using UnityEngine;

public class Tween<T> : ITween
{
    private T startValue;
    private T endValue;
    private float duration;
    private Action<T> onTweenUpdate;
    private float elapsedTime;
    private float delayElapsedTime = 0f;
    private int loopsCompleted = 0;
    private bool reverse = false;
    private bool pingPong = false;
    private int loopCount = 1;
    private float percentThreshold = -1f;

    private Action onUpdate;
    private Action onPercentCompleted;

    private EaseType easeType = EaseType.Linear; // defaults ease type to linear

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
        if (!IsPaused)
        {
            if (IgnoreTimeScale) delayElapsedTime += Time.unscaledDeltaTime;
            else delayElapsedTime += Time.deltaTime;

            if (delayElapsedTime >= DelayTime)
            {
                if (IsComplete) return;

                if (IsTargetDestroyed())
                {
                    FullKill();
                    return;
                }

                if (IgnoreTimeScale) elapsedTime += Time.unscaledDeltaTime;
                else elapsedTime += Time.deltaTime;

                float t = elapsedTime / duration;
                float easedT = Ease(easeType, t);

                T currentValue;

                if (reverse) currentValue = Interpolate(endValue, startValue, easedT);
                else currentValue = Interpolate(startValue, endValue, easedT);

                onUpdate?.Invoke();
                onTweenUpdate?.Invoke(currentValue);

                if (percentThreshold >= 0f && t >= percentThreshold)
                {
                    onPercentCompleted?.Invoke();
                    percentThreshold = -1f; // so that it doesn't call every frame
                }

                if (elapsedTime >= duration)
                {
                    loopsCompleted++;
                    elapsedTime = 0;

                    if (pingPong) reverse = !reverse;

                    // loopCount defaults to 1 so this will be called even if you don't set ping pongs.
                    if (loopCount > 0 && loopsCompleted >= loopCount)
                    {
                        OnCompleteKill();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Lerps <paramref name="start"/> to <paramref name="end"/> within <paramref name="t"/> seconds. 
    /// Will throw an exception if the given type hasn't been implemented.
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

    /// <summary>
    /// Checks if a target is destroyed.
    /// </summary>
    /// <returns></returns>
    public bool IsTargetDestroyed()
    {
        if(Target is MonoBehaviour monoB && monoB == null)
        {
            return true;
        }

        if(Target is GameObject gameObj && gameObj == null)
        {
            return true;
        }
        
        if(Target is Delegate del && del == null)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Kills the tween when completed.
    /// </summary>
    public void OnCompleteKill()
    {
        IsComplete = true;
        onUpdate = null;
        onTweenUpdate = null;
        onPercentCompleted = null;
    }

    /// <summary>
    /// Kills the tween of an object that was destroyed before the tween finishes.
    /// </summary>
    public void FullKill()
    {
        OnCompleteKill();
        WasKilled = true;
        onComplete = null;
    }

    /// <summary>
    /// Pauses the tween.
    /// </summary>
    public void Pause()
    {
        IsPaused = true;
    }

    /// <summary>
    /// Unpauses the tween.
    /// </summary>
    public void Resume()
    {
        IsPaused = false;
    }

    /// <summary>
    /// Sets the ease type of the tween.
    /// </summary>
    /// <param name="easeType"></param>
    /// <returns></returns>
    public Tween<T> SetEase(EaseType easeType)
    {
        this.easeType = easeType;
        return this;
    }

    /// <summary>
    /// Replays the tween reversed after it is completed <paramref name="loopCount"/> amount of times.
    /// Set <paramref name="loopCount"/> to -1 for infinite loops.
    /// </summary>
    /// <param name="loopCount"></param>
    /// <returns></returns>
    public Tween<T> SetPingPong(int loopCount = 1)
    {
        this.loopCount = loopCount;
        pingPong = true;
        return this;
    }

    /// <summary>
    /// Set a function to call while the tween is playing.
    /// </summary>
    /// <param name="onUpdate"></param>
    /// <returns></returns>
    public Tween<T> SetOnUpdate(Action onUpdate)
    {
        this.onUpdate = onUpdate;
        return this;
    }

    /// <summary>
    /// Set a function to call when when the tween is completed.
    /// </summary>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public Tween<T> SetOnComplete(Action onComplete)
    {
        this.onComplete = onComplete;
        return this;
    }

    /// <summary>
    /// Set a function to call at a specific time. Use values from 0 to 1 for <paramref name="percentCompleted"/>.
    /// E.g. set <paramref name="percentCompleted"/> to 0.5 if the function should be called halfway through. 
    /// Or to 0.7 if it should be called at 70% completion.
    /// </summary>
    /// <param name="percentCompleted"></param>
    /// <param name="onPercentCompleted"></param>
    /// <returns></returns>
    public Tween<T> SetOnPercentComplete(float percentCompleted, Action onPercentCompleted)
    {
        percentThreshold = Mathf.Clamp01(percentCompleted);
        this.onPercentCompleted = onPercentCompleted;
        return this;
    }

    /// <summary>
    /// Ignores the time scale if set to true.
    /// </summary>
    /// <param name="ignoreTimeScale"></param>
    /// <returns></returns>
    public Tween<T> SetIgnoreTimeScale(bool ignoreTimeScale = false)
    {
        IgnoreTimeScale = ignoreTimeScale;
        return this;
    }

    /// <summary>
    /// Sets the amount of time to delay the tween action by <paramref name="delayTime"/> seconds.
    /// </summary>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    public Tween<T> SetStartDelay(float delayTime)
    {
        DelayTime = delayTime;
        return this;
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

    public static float Ease(EaseType easeType, float t)
    {
        switch (easeType)
        {
            case EaseType.Linear: return Linear(t);
            case EaseType.ExpoEaseIn: return ExpoEaseIn(t);
            case EaseType.ExpoEaseOut: return ExpoEaseOut(t);
            case EaseType.ExpoEaseInOut: return ExpoEaseInOut(t);
            case EaseType.ExpoEaseOutIn: return ExpoEaseOutIn(t);
            case EaseType.CircEaseIn: return CircEaseIn(t);
            case EaseType.CircEaseOut: return CircEaseOut(t);
            case EaseType.CircEaseInOut: return CircEaseInOut(t);
            case EaseType.CircEaseOutIn: return CircEaseOutIn(t);
            case EaseType.QuadEaseIn: return QuadEaseIn(t);
            case EaseType.QuadEaseOut: return QuadEaseOut(t);
            case EaseType.QuadEaseInOut: return QuadEaseInOut(t);
            case EaseType.QuadEaseOutIn: return QuadEaseOutIn(t);
            case EaseType.SineEaseIn: return SineEaseIn(t);
            case EaseType.SineEaseOut: return SineEaseOut(t);
            case EaseType.SineEaseInOut: return SineEaseInOut(t);
            case EaseType.SineEaseOutIn: return SineEaseOutIn(t);
            case EaseType.CubicEaseIn: return CubicEaseIn(t);
            case EaseType.CubicEaseOut: return CubicEaseOut(t);
            case EaseType.CubicEaseInOut: return CubicEaseInOut(t);
            case EaseType.CubicEaseOutIn: return CubicEaseOutIn(t);
            case EaseType.QuartEaseIn: return QuartEaseIn(t);
            case EaseType.QuartEaseOut: return QuartEaseOut(t);
            case EaseType.QuartEaseInOut: return QuartEaseInOut(t);
            case EaseType.QuartEaseOutIn: return QuartEaseOutIn(t);
            case EaseType.QuintEaseIn: return QuintEaseIn(t);
            case EaseType.QuintEaseOut: return QuintEaseOut(t);
            case EaseType.QuintEaseInOut: return QuintEaseInOut(t);
            case EaseType.QuintEaseOutIn: return QuintEaseOutIn(t);
            case EaseType.ElasticEaseIn: return ElasticEaseIn(t);
            case EaseType.ElasticEaseOut: return ElasticEaseOut(t);
            case EaseType.ElasticEaseInOut: return ElasticEaseInOut(t);
            case EaseType.ElasticEaseOutIn: return ElasticEaseOutIn(t);
            case EaseType.BounceEaseIn: return BounceEaseIn(t);
            case EaseType.BounceEaseOut: return BounceEaseOut(t);
            case EaseType.BounceEaseInOut: return BounceEaseInOut(t);
            case EaseType.BounceEaseOutIn: return BounceEaseOutIn(t);
            case EaseType.BackEaseIn: return BackEaseIn(t);
            case EaseType.BackEaseOut: return BackEaseOut(t);
            case EaseType.BackEaseInOut: return BackEaseInOut(t);
            case EaseType.BackEaseOutIn: return BackEaseOutIn(t);
            default: return 0f;
        }
    }
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