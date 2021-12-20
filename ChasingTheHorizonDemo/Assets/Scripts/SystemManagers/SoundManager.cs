using UnityEngine;

//Sound Manager handles sound effects
//The PlaySound function can be called from any script as needed
//There should only be 1 SoundManager in a given scene
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    private AudioSource source;

    [SerializeField] private AudioClip[] soundFXs = null;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }

    public void PlayFX(int fx)
    {
        source.PlayOneShot(soundFXs[fx]);
    }
}
