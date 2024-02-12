using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButtonLoader : MonoBehaviour
{
    [SerializeField] private Unit[] units;

    private void Start()
    {
        for(int childIndex = 0; childIndex < transform.childCount; childIndex++) 
        {
            Destroy(transform.GetChild(childIndex).gameObject);
        }

        List<Button> buttons = new List<Button>();

        for(int i= 0; i < units.Length; i++)
        {
            GameObject buttonToAdd = new GameObject();
            buttonToAdd.AddComponent<Button>();
            buttonToAdd.AddComponent<RectTransform>();
            buttons.Add(buttonToAdd.GetComponent<Button>());
        }

        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].transform.SetParent(transform, false);
            GameObject buttonText = new GameObject();
            buttonText.AddComponent<Text>();
            buttonText.GetComponent<Text>().text = units[i].name;
            buttons[i].targetGraphic = buttonText.GetComponent<Text>();
            buttonText.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            buttonText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

            buttonText.transform.SetParent(buttons[i].transform, false);
        }
    }
}
