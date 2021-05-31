using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{
    public static ItemLibrary instance { get; private set; }
    
    private void Awake()
    {
        instance = this;
    }

    public void UseItem(int ID)
    {
        switch(ID)
        {
            //Vulnerary: Restore 10 Health
            case 0:
                Debug.Log("Vulnerary used");
                break;
        }
    }
}
