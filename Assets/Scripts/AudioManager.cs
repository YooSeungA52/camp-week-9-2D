using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip clickSound;
    public AudioClip buySound;
    public AudioClip upgradeSound;
    public AudioClip failSound;

    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

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

    private void Start()
    {
        bgmSource.Play(); // BGM 재생

        // 볼륨 슬라이더 초기화
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f); // 저장된 볼륨 불러오기, 없으면 기본값 0.5f로 설정
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        UpdateBGMVolume();
        UpdateSFXVolume();
    }

    public void UpdateBGMVolume() // BGM 볼륨 조절 메서드
    {
        float volume = bgmVolumeSlider.value;
        PlayerPrefs.SetFloat("BGMVolume", volume); // 설정된 볼륨 저장
        bgmSource.volume = volume; // BGM 볼륨 조절
    }

    public void UpdateSFXVolume() // SFX 볼륨 조절 메서드
    {
        float volume = sfxVolumeSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", volume); // 설정된 볼륨 저장
        sfxSource.volume = volume; // SFX 볼륨 조절
    }

    public void PlayClickSound() // 클릭 시 효과음
    {
        sfxSource.PlayOneShot(clickSound);
    }

    public void PlayBuySound() // 아이템 구매 시 효과음
    {
        sfxSource.PlayOneShot(buySound);
    }

    public void PlayUpgradeSound() // 아이템 업글 시 효과음
    {
        sfxSource.PlayOneShot(upgradeSound);
    }

    public void PlayFailSound() // 코인 부족 시 효과음
    {
        sfxSource.PlayOneShot(failSound);
    }
}
