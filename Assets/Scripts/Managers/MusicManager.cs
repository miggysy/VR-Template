using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> music = new List<AudioClip>();
    private AudioSource audioSource;
    private int currentClipIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (music.Count > 0)
            PlayNextClip();
        else
            Debug.LogWarning("No audio clips assigned to the AudioPlayer!");
    }

    private void PlayNextClip()
    {
        if (music.Count == 0)
            return;

        if (currentClipIndex >= music.Count)
            currentClipIndex = 0;

        audioSource.clip = music[currentClipIndex];
        audioSource.Play();
        currentClipIndex++;
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }
}