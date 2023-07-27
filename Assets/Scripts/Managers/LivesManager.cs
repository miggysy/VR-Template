using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private int totalLives;
    [SerializeField] private int currentLives;
    public static event Action<int> onCurrentLivesChanged;

    private void InitializeLives() 
    {
        currentLives = totalLives;
        onCurrentLivesChanged?.Invoke(currentLives);
    }

    private void LoseLife() 
    {
        currentLives--;
        CheckGameOver();
        onCurrentLivesChanged?.Invoke(currentLives);
    }

    private void CheckGameOver()
    {
        if(currentLives <= 0) 
        {
            currentLives = 0;
            GameManager.onGameOver?.Invoke();
        }
    }

    private void OnEnable()
    {
        GameManager.onStartGame += InitializeLives;
        GameManager.onCustomerLeft += LoseLife;
    }

    private void OnDisable()
    {
        GameManager.onStartGame -= InitializeLives;
        GameManager.onCustomerLeft -= LoseLife;
    }
}
