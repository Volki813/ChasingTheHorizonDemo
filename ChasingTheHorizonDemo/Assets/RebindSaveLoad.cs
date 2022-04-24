using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : MonoBehaviour
{
    public InputActionAsset cursorActions;
    public InputActionAsset cutSceneActions;

    public void OnEnable()
    {
        var cursorRebinds = PlayerPrefs.GetString("cursorRebinds");
        if (!string.IsNullOrEmpty(cursorRebinds))
            cursorActions.FindActionMap("MapScene").LoadBindingOverridesFromJson(cursorRebinds);

        var cursorUIRebinds = PlayerPrefs.GetString("cursorUIRebinds");
        if (!string.IsNullOrEmpty(cursorUIRebinds))
            cursorActions.FindActionMap("UI").LoadBindingOverridesFromJson(cursorUIRebinds);

        var cutSceneRebinds = PlayerPrefs.GetString("cutSceneRebinds");
        if (!string.IsNullOrEmpty(cutSceneRebinds))
            cutSceneActions.FindActionMap("Cutscenes").LoadBindingOverridesFromJson(cutSceneRebinds);

        var mapDialogueRebinds = PlayerPrefs.GetString("mapDialogueRebinds");
        if (!string.IsNullOrEmpty(cutSceneRebinds))
            cutSceneActions.FindActionMap("Maps").LoadBindingOverridesFromJson(mapDialogueRebinds);
    }

    public void OnDisable()
    {
        var cursorRebinds = cursorActions.FindActionMap("MapScene").SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("cursorRebinds", cursorRebinds);

        var cursorUIRebinds = cursorActions.FindActionMap("UI").SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("cursorUIRebinds", cursorUIRebinds);

        var cutSceneRebinds = cutSceneActions.FindActionMap("Cutscenes").SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("cutSceneRebinds", cutSceneRebinds);

        var mapDialogueRebinds = cutSceneActions.FindActionMap("Maps").SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("mapDialogueRebinds", mapDialogueRebinds);
    }
}
