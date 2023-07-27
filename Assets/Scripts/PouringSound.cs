using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringSound : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    private Ladle ladle;
    private AudioSource audioSource;
    private float targetVolume;
    private bool pouring;

    void Awake()
    {
        ladle = transform.parent.GetComponentInChildren<Ladle>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.mute = FindObjectOfType<SoundManager>().SoundMuted;
        targetVolume = audioSource.volume;
        audioSource.volume = 0f;    
    }

    void StartPouring() => pouring = true;

    void StopPouring() => pouring  = false;

    // Update is called once per frame
    void Update()
    {
        pouring = ladle.IsPouring;

        if(pouring)
        {
            if(audioSource.volume >= targetVolume) audioSource.volume = targetVolume;
            else audioSource.volume += Time.deltaTime * fadeSpeed;
        }
        else
        {
            if(audioSource.volume <= 0f) audioSource.volume = 0f;
            audioSource.volume -= Time.deltaTime * fadeSpeed;
        }
    }

    void MuteSound() => audioSource.mute = true;
    void UnmuteSound() => audioSource.mute = false;

    void OnEnable()
    {
        SoundManager.onMuteSound += MuteSound;
        SoundManager.onUnmuteSound += UnmuteSound;
    }
    
    void OnDisable()
    {
        SoundManager.onMuteSound -= MuteSound;
        SoundManager.onUnmuteSound -= UnmuteSound;
    }
}
