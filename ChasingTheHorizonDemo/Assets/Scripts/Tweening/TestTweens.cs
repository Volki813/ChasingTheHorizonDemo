using UnityEngine;
using UnityEngine.InputSystem;

public class TestTweens : MonoBehaviour
{
    float sec = 0;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TweenManager.TweenScale(gameObject, new Vector3(2,2,2), new Vector3(1,1,1), 1)
                .SetEase(EaseType.ElasticEaseOut)
                .SetPingPong(3)
                .SetOnPercentComplete(0.5f, On50Percent)
                .SetOnUpdate(Count);
        }
    }

    public void On50Percent()
    {
        print("hi");
    }

    public void Count()
    {
        sec += Time.deltaTime;
        print(sec);
    }
}
