using UnityEngine;
using UnityEngine.InputSystem;

public class TestTweens : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TweenManager.TweenSpriteAlpha(gameObject, 1, 0, 0.5f);
        }
    }
}
