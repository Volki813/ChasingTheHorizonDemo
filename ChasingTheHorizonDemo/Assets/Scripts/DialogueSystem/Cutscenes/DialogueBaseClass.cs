using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//The base class for all Dialogue Lines. Defines the parameters of a Dialogue Line
namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {        
        public bool finished { get; private set; }        

        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay, AudioClip sound)
        {
            textHolder.color = textColor;
            textHolder.font = textFont;

            if(sound != null)
            {
                SoundManager.instance.PlaySound(sound);
                yield return new WaitForSeconds(sound.length);
            }

            for (int i=0; i<input.Length; i++)
            {
                textHolder.text += input[i];
                SoundManager.instance.PlayFX(7);
                yield return new WaitForSeconds(delay);
            }            

            finished = true;
        }
    }
}
