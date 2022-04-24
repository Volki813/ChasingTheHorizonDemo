using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetAllBindings : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset cursorActions = null;

    [SerializeField]
    private InputActionAsset cutsceneActions = null;

    public void ResetBindings()
    {
        foreach(InputActionMap map in cursorActions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
        foreach(InputActionMap map in cutsceneActions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
        PlayerPrefs.DeleteKey("cursorRebinds");
        PlayerPrefs.DeleteKey("cursorUIRebinds");
        PlayerPrefs.DeleteKey("cutSceneRebinds");
        PlayerPrefs.DeleteKey("mapDialogueRebinds");
    }
}
