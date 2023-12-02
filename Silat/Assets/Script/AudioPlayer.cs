using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("musicVolume");
            myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
            audioSource.volume = volume;

            // Play your audio here using AudioSource
            audioSource.Play();
        }
    }
}
