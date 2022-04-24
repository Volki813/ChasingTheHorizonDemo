using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebindingDisplay : MonoBehaviour
{
    [SerializeField] private GameObject volumePanel = null;
    [SerializeField] private GameObject keyboardPanel = null;
    [SerializeField] private GameObject gamepadPanel = null;

    public void VolumeTab()
    {
        if(volumePanel.activeSelf)
        {
            volumePanel.SetActive(false);
        }
        else
        {
            keyboardPanel.SetActive(false);
            gamepadPanel.SetActive(false);
            volumePanel.SetActive(true);
        }
    }

    public void KeyboardTab()
    {
        if(keyboardPanel.activeSelf)
        {
            keyboardPanel.SetActive(false);
        }
        else
        {
            gamepadPanel.SetActive(false);
            volumePanel.SetActive(false);
            keyboardPanel.SetActive(true);
        }
    }

    public void GamepadTab()
    {
        if (gamepadPanel.activeSelf)
        {
            gamepadPanel.SetActive(false);
        }
        else
        {
            volumePanel.SetActive(false);
            keyboardPanel.SetActive(false);
            gamepadPanel.SetActive(true);
        }
    }
}
