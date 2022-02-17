using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//The Dialogue Holder holds all the Dialogue blocks and displays them on screen
//Used for cutscenes
namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        //VARIBALES 
        private bool buttonPressed = false;
        private DialogueLine previousLine = null;
        private DialogueLine currentLine = null;
        private DialogueLine savedLine = null;
        [Header("Dialogue Boxs")]
        [SerializeField] private Vector2 box1 = new Vector3(0, 0, 0);
        [SerializeField] private Vector2 box2 = new Vector3(0, 0, 0);

        private void Awake()
        {
            StartCoroutine(DialogueSequence());
        }

        public void StartDialogue()
        {
            StartCoroutine(DialogueSequence());
        }

        private IEnumerator DialogueSequence()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            for(int i=0; i<transform.childCount; i++)
            {
                //Checks if there is a current dialogue line
                if (currentLine != null)
                {
                    previousLine = currentLine;
                    if(transform.GetChild(i).GetComponent<DialogueLine>())
                    {
                        currentLine = transform.GetChild(i).GetComponent<DialogueLine>();
                    }
                }
                else
                {
                    if (transform.GetChild(i).GetComponent<DialogueLine>())
                    {
                        currentLine = transform.GetChild(i).GetComponent<DialogueLine>();
                    }
                }                

                //Checks if there is a saved dialogue line
                if (savedLine != null)
                {
                    //Checks if the saved dialogue line is in the same textbox as the current dialogue line
                    if (savedLine.textBox1 && currentLine.textBox1 || savedLine.textBox2 && currentLine.textBox2)
                    {
                        savedLine.gameObject.SetActive(false);
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                }

                //Checks if there is a previous dialogue line
                if (previousLine != null)
                {
                    //Checks if the previous dialogue line is in the textbox opposite of the current dialogue line
                    if(previousLine.textBox1 && currentLine.textBox2 || previousLine.textBox2 && currentLine.textBox1)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                        savedLine = previousLine;
                    }
                    else
                    {
                        Deactivate();
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                }                
                else
                {
                    Deactivate();
                    transform.GetChild(i).gameObject.SetActive(true);
                }

                //Checks if it's a transition line, which changes the scene 
                if (transform.GetChild(i).GetComponent<TransitionLine>() == true)
                {
                    transform.GetChild(i).GetComponent<TransitionLine>().ChangeScene();
                    break;
                }

                //Checks which Dialogue Box to place the dialogue in 
                else if (transform.GetChild(i).GetComponent<DialogueLine>().textBox1 == true)
                {
                    transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = box1;
                }
                else if(transform.GetChild(i).GetComponent<DialogueLine>().textBox2 == true)
                {
                    transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = box2;
                }

                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
                buttonPressed = false;
                yield return new WaitUntil(() => buttonPressed == true);
            }
        }

        public void NextButton(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                buttonPressed = true;
            }
        }

        public void SkipButton(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        private void Deactivate()
        {
            if(previousLine != null && !previousLine.lastLine)
            {
                previousLine.gameObject.SetActive(false);
            }
        }

        public void ClearHolder()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}