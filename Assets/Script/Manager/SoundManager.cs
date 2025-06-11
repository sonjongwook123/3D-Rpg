using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] sfxClips;

    public enum BGM_Type { None, MainTheme, BattleTheme, ResultTheme }
    public enum SFX_Type { None, Attack }

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
        }
    }

    public void PlayBGM(BGM_Type bgmType)
    {
        if (bgmType == BGM_Type.None)
        {
            musicSource.Stop();
            return;
        }

        int index = (int)bgmType - 1;
        if (index >= 0 && index < bgmClips.Length && bgmClips[index] != null)
        {
            musicSource.clip = bgmClips[index];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM clip not found for type: {bgmType}");
        }
    }

    public void PlaySFX(SFX_Type sfxType, float volume = 1f, float pitch = 1f)
    {
        if (sfxType == SFX_Type.None) return;

        int index = (int)sfxType - 1;
        if (index >= 0 && index < sfxClips.Length && sfxClips[index] != null)
        {
            sfxSource.volume = volume;
            sfxSource.pitch = pitch;
            sfxSource.PlayOneShot(sfxClips[index]);
        }
        else
        {
            Debug.LogWarning($"SFX clip not found for type: {sfxType}");
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StopAllSounds()
    {
        musicSource.Stop();
        sfxSource.Stop();
    }
}