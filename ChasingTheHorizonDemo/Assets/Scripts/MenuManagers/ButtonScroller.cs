using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ButtonScroller : MonoBehaviour
{
    private PlayerInput cursorControls;

    [SerializeField] private float lerpTime;
    [SerializeField] private GameObject scrollContainer;
    private ScrollRect scrollRect;
    private Button[] buttons;
    private int index;
    private float verticalPosition;
    private bool up;
    private bool down;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        buttons = scrollContainer.GetComponentsInChildren<Button>();
        cursorControls = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        buttons[index].Select();
        verticalPosition = 1f - ((float)index / (buttons.Length - 1));
    }

    private void Update()
    {
        up = cursorControls.actions["Navigate"].ReadValue<Vector2>().y > 0.001 && cursorControls.actions["Navigate"].WasPressedThisFrame();
        down = cursorControls.actions["Navigate"].ReadValue<Vector2>().y < -0.001 && cursorControls.actions["Navigate"].WasPressedThisFrame();

        if (up ^ down) 
        {
            if (up)
            {
                index = Mathf.Clamp(index - 1, 0, buttons.Length - 1);
            }
            else
            {
                index = Mathf.Clamp(index + 1, 0, buttons.Length - 1);
            }
            buttons[index].Select();
            verticalPosition = 1f - ((float)index / (buttons.Length - 1));
        }
        scrollRect.verticalNormalizedPosition = Mathf.Lerp(scrollRect.verticalNormalizedPosition, verticalPosition, Time.deltaTime / lerpTime);
    }
}
