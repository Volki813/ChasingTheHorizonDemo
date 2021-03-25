using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class TransitionLine : MonoBehaviour
    {
        [SerializeField] private string nextScene;
        public void ChangeScene()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
