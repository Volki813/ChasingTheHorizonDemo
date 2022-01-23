using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider musicSlider = null;
    [SerializeField]
    private Slider soundFXSlider = null;
    [SerializeField]
    private Slider gridSlider = null;

    public AudioSource musicVolume = null;
    public AudioSource soundFXVolume = null;

    private void Start()
    {
        musicSlider.value = musicVolume.volume;
        soundFXSlider.value = soundFXVolume.volume;
    }
    private void OnEnable()
    {
        StartCoroutine(HighlightSlider());
    }
    private void Update()
    {
        musicVolume.volume = musicSlider.value;
        soundFXVolume.volume = soundFXSlider.value;
        ChangeTiles((int)gridSlider.value);
    }
    private void ChangeTiles(int value)
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            tile.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, (byte)value);
        }
    }
    private IEnumerator HighlightSlider()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(musicSlider.gameObject);
    }
}
