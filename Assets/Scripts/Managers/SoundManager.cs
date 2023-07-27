using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private bool soundMuted;
    public bool SoundMuted { get => soundMuted; }
    public static event Action onMuteSound;
    public static event Action onUnmuteSound;

    public void MuteSound()
    {
        soundMuted = true;
        onMuteSound?.Invoke();
    }
    public void UnmuteSound() 
    {
        soundMuted = false;
        onUnmuteSound?.Invoke();
    }

}
