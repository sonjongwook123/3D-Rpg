using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; // 배경음악용 AudioSource
    [SerializeField] private AudioSource sfxSource;   // 효과음용 AudioSource

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] bgmClips; // 배경음악 클립 배열
    [SerializeField] private AudioClip[] sfxClips; // 효과음 클립 배열

    // 사운드 식별을 위한 enum (선택 사항, ScriptableObject 사용 시 다를 수 있음)
    public enum BGM_Type { None, MainTheme, BattleTheme, ResultTheme }
    public enum SFX_Type { None, Click, Hit, Explosion, Footstep }

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

    // 배경음악 재생
    public void PlayBGM(BGM_Type bgmType)
    {
        if (bgmType == BGM_Type.None)
        {
            musicSource.Stop();
            return;
        }

        int index = (int)bgmType - 1; // Enum의 첫 번째 값이 None이므로 -1
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

    // 효과음 재생 (단발성)
    public void PlaySFX(SFX_Type sfxType, float volume = 1f, float pitch = 1f)
    {
        if (sfxType == SFX_Type.None) return;

        int index = (int)sfxType - 1; // Enum의 첫 번째 값이 None이므로 -1
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

    // 배경음악 볼륨 조절
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // 효과음 볼륨 조절
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // 배경음악 정지
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // 모든 사운드 정지 (필요 시)
    public void StopAllSounds()
    {
        musicSource.Stop();
        sfxSource.Stop();
    }
}