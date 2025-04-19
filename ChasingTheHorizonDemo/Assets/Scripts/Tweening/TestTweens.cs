using UnityEngine;
using UnityEngine.InputSystem;

public class TestTweens : MonoBehaviour
{
    public EaseType easeType = EaseType.Linear;

    private Vector3 startPos;
    private Vector3 startScale;

    private Tween<Vector3> tween1, tween2;

    private void Start()
    {
        startScale = transform.localScale;
        startPos = transform.position;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            tween1 = TweenManager.TweenScale(gameObject, startScale, startScale * 2, 2).SetEase(easeType).SetPingPong(-1);

            tween2 = TweenManager.TweenPosition(gameObject, startPos, new Vector3(5, 0, 0), 1)
                .SetEase(easeType)
                .SetPingPong(7);
        }
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            tween2.CompleteTween();
        }
    }
}
