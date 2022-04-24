using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class ImageLine : DialogueBaseClass
    {
        private Image image = null;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void DisplayImage()
        {
            print("Image displaying");
        }
    }
}
