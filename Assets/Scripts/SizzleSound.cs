using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizzleSound : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    private Food food;
    private AudioSource audioSource;
    private float targetVolume;
    private bool sizzle;

    void Awake()
    {
        food = GetComponentInParent<Food>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.mute = FindObjectOfType<SoundManager>().SoundMuted;
        targetVolume = audioSource.volume;
        audioSource.volume = 0f;    
    }

    void StartSizzling() => sizzle = true;

    void StopSizzling() => sizzle  = false;

    // Update is called once per frame
    void Update()
    {
        if(sizzle)
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
        food.onStartCooking += StartSizzling;
        food.onStopCooking += StopSizzling;
        SoundManager.onMuteSound += MuteSound;
        SoundManager.onUnmuteSound += UnmuteSound;
    }
    
    void OnDisable()
    {
        food.onStartCooking -= StartSizzling;
        food.onStopCooking -= StopSizzling;
        SoundManager.onMuteSound -= MuteSound;
        SoundManager.onUnmuteSound -= UnmuteSound;
    }
}
