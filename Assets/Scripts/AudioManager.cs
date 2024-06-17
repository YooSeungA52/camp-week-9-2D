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
        bgmSource.Play(); // BGM ���

        // ���� �����̴� �ʱ�ȭ
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f); // ����� ���� �ҷ�����, ������ �⺻�� 0.5f�� ����
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        UpdateBGMVolume();
        UpdateSFXVolume();
    }

    public void UpdateBGMVolume() // BGM ���� ���� �޼���
    {
        float volume = bgmVolumeSlider.value;
        PlayerPrefs.SetFloat("BGMVolume", volume); // ������ ���� ����
        bgmSource.volume = volume; // BGM ���� ����
    }

    public void UpdateSFXVolume() // SFX ���� ���� �޼���
    {
        float volume = sfxVolumeSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", volume); // ������ ���� ����
        sfxSource.volume = volume; // SFX ���� ����
    }

    public void PlayClickSound() // Ŭ�� �� ȿ����
    {
        sfxSource.PlayOneShot(clickSound);
    }

    public void PlayBuySound() // ������ ���� �� ȿ����
    {
        sfxSource.PlayOneShot(buySound);
    }

    public void PlayUpgradeSound() // ������ ���� �� ȿ����
    {
        sfxSource.PlayOneShot(upgradeSound);
    }

    public void PlayFailSound() // ���� ���� �� ȿ����
    {
        sfxSource.PlayOneShot(failSound);
    }
}
