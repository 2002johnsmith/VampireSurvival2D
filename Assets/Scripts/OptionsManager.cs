using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    private void Start()
    {
        float savedMusic = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
        float savedSfx = PlayerPrefs.GetFloat(SfxVolumeKey, 0.5f);

        musicSlider.value = savedMusic;
        sfxSlider.value = savedSfx;

        AudioManager.Instance.SetMusicVolume(savedMusic);
        AudioManager.Instance.SetSfxVolume(savedSfx);
    }

    public void OnMusicChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnSfxChanged(float value)
    {
        AudioManager.Instance.SetSfxVolume(value);
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, musicSlider.value);
        PlayerPrefs.SetFloat(SfxVolumeKey, sfxSlider.value);
        PlayerPrefs.Save();

        Debug.Log("Opciones de audio guardadas");
    }
}
