using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("音效")]
    public AudioClip eatSound;
    public AudioClip gameOverSound;
    public AudioClip buttonSound;
    public AudioClip levelUpSound;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    // 吃食物
    public void PlayEatSound()
    {
        audioSource.PlayOneShot(eatSound);
    }

    // GameOver
    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    // 按钮
    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }

    // 升级
    public void PlayLevelUpSound()
    {
        audioSource.PlayOneShot(levelUpSound);
    }
}
