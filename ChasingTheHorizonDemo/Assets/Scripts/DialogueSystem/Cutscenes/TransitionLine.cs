using UnityEngine;
using UnityEngine.SceneManagement;


//A type of Dialogue Line that will change the scene to the Next Scene variable
//Used at the end of a chunk of dialogue
namespace DialogueSystem
{
    public class TransitionLine : MonoBehaviour
    {
        [SerializeField] private string nextScene = null;

        public void ChangeScene()
        {
            MusicPlayer.instance.FadeMusic(false);
            Invoke("DelayedChangeScene", 1.5f);
        }

        private void DelayedChangeScene()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
