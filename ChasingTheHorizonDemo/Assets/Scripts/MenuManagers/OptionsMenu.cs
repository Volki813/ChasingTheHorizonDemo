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

    private void Awake()
    {
        //musicVolume = FindObjectOfType<MusicPlayer>().GetComponent<AudioSource>();
        soundFXVolume = FindObjectOfType<SoundManager>().GetComponent<AudioSource>();

        //musicSlider.value = musicVolume.volume;
        soundFXSlider.value = soundFXVolume.volume;
    }
    private void OnEnable()
    {
        SoundManager.instance.PlayFX(11);
        StartCoroutine(HighlightSlider());
    }
    private void Update()
    {
        //musicVolume.volume = musicSlider.value;
        soundFXVolume.volume = soundFXSlider.value;
        OpacityControl(gridSlider.value);
    }

    private void OpacityControl(float opacityLevel)
    {
        switch(opacityLevel){
            case 1:
                ChangeTiles(0);
                break;
            case 2:
                ChangeTiles(25.5f);
                break;
            case 3:
                ChangeTiles(51f);
                break;
            case 4:
                ChangeTiles(76.5f);
                break;
            case 5:
                ChangeTiles(102);
                break;
            case 6:
                ChangeTiles(127.5f);
                break;
            case 7:
                ChangeTiles(153);
                break;
            case 8:
                ChangeTiles(178.5f);
                break;
            case 9:
                ChangeTiles(204);
                break;
            case 10:
                ChangeTiles(255);
                break;
        }
    }
    private void ChangeTiles(float value)
    {
        foreach(SelectableTile tile in FindObjectsOfType<SelectableTile>())
        {
            tile.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, (byte)value);
        }
    }
    private IEnumerator HighlightSlider()
    {
        yield return new WaitForSeconds(0.17f);
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(musicSlider.gameObject);
    }
}
