using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] tracks = null;
    private AudioSource source = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(tracks[SceneManager.GetActiveScene().buildIndex]){
            source.clip = tracks[SceneManager.GetActiveScene().buildIndex];
            source.Play();
        }
        else if(tracks[SceneManager.GetActiveScene().buildIndex] == null && SceneManager.GetActiveScene().buildIndex > 0)
        {
            GetComponent<AudioSource>().clip = null;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
