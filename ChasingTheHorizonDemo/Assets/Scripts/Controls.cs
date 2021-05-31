using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public static Controls instance { get; private set; }

    public KeyCode confirmButton;
    public KeyCode cancelButton;
    public KeyCode lButton;
    public KeyCode rButton;

    public KeyCode start;
    public KeyCode select;

    private void Awake()
    {
        instance = this;
    }
}
