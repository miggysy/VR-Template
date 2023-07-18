using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private int totalLives;
    [SerializeField] private int currentLives;
    public static event Action onInitializeLives;
    public static event Action onLoseLife;

    private void Start()
    {
        InitializeLives();
    }

    private void InitializeLives() 
    {
        currentLives = totalLives;
        onInitializeLives?.Invoke();
    }

    private void LoseLife() 
    {
        currentLives--;
        CheckGameOver();
        onLoseLife?.Invoke();
    }

    private void CheckGameOver()
    {
        if(currentLives <= 0) GameManager.onGameOver?.Invoke();
    }

    private void OnEnable()
    {
        GameManager.onCustomerLeft += LoseLife;
        GameManager.onOrderWrong += LoseLife;
    }

    private void OnDisable()
    {
        GameManager.onCustomerLeft -= LoseLife;
        GameManager.onOrderWrong -= LoseLife;
    }
}
