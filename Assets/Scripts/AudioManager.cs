using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip clickSound;
    public AudioClip buySound;
    public AudioClip upgradeSound;

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
        bgmSource.Play(); // BGM Àç»ý
    }

    public void PlayClickSound()
    {
        sfxSource.PlayOneShot(clickSound);
    }

    public void PlayBuySound()
    {
        sfxSource.PlayOneShot(buySound);
    }

    public void PlayUpgradeSound()
    {
        sfxSource.PlayOneShot(upgradeSound);
    }
}
