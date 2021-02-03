using System.Collections;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        [Header("Dialogue Boxs")]
        [SerializeField] private Vector3 box1;
        [SerializeField] private Vector3 box2;

        private void Awake()
        {
            StartCoroutine(DialogueSequence());
        }

        private IEnumerator DialogueSequence()
        {
            for(int i=0; i<transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                if(transform.GetChild(i).GetComponent<DialogueLine>().textBox1 == true)
                {
                    transform.GetChild(i).GetComponent<RectTransform>().localPosition = box1;
                }
                else
                {
                    transform.GetChild(i).GetComponent<RectTransform>().localPosition = box2;
                }
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
        }

        private void Deactivate()
        {
            for(int i=0; i<transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}