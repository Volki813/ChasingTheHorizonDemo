using UnityEngine;

//Sound Manager handles sound effects
//The PlaySound function can be called from any script as needed
//There should only be 1 SoundManager in a given scene
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
