using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class ActionMenu : MonoBehaviour
{
    // [SerializeField] private List<Button> buttonList;
    // private List<Button> instantiatedButtonList = new List<Button>();
    

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        //foreach (Button button in buttonList)
        //{
        //    Button b = Instantiate(button, transform);
        //    instantiatedButtonList.Add(b);
        //    b.Select();
        //}
    }

    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    //private void OnDisable()
    //{
    //    foreach (Button button in instantiatedButtonList)
    //    {
    //        Destroy(button);
    //    }
    //}
}
