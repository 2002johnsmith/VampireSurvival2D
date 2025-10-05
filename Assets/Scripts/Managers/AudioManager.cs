using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips de Música")]
    [SerializeField] private List<AudioClip> musicClips;

    [Header("Clips de Efectos")]
    [SerializeField] private List<AudioClip> sfxClips;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Restaurar volúmenes guardados
        float musicVol = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
        float sfxVol = PlayerPrefs.GetFloat(SfxVolumeKey, 0.5f);
        SetMusicVolume(musicVol);
        SetSfxVolume(sfxVol);
    }

    public void PlayMusic(int index)
    {
        if (index < 0 || index >= musicClips.Count)
        {
            Debug.LogWarning("Índice de música inválido: " + index);
            return;
        }

        AudioClip clip = musicClips[index];
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(int index)
    {
        if (index < 0 || index >= sfxClips.Count)
        {
            Debug.LogWarning("Índice de SFX inválido: " + index);
            return;
        }

        sfxSource.PlayOneShot(sfxClips[index]);
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
    }

    public void SetSfxVolume(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat(SfxVolumeKey, value);
    }
}