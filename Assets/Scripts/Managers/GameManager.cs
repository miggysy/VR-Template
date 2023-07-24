using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isPaused;
    [Header("Menu References")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject leftRayInteractor;
    [SerializeField] private GameObject rightRayInteractor;
    [SerializeField] private GameObject leftDirectInteractor;
    [SerializeField] private GameObject rightDirectInteractor;
    [SerializeField] private GameObject pausedText;
    public bool IsPaused { get => isPaused; }
    public delegate void GameEvent();
    public static GameEvent onStartGame;    //Call when the game starts
    public static GameEvent onSubmittedOrder;   //Call when the order is submitted
    public static GameEvent onCustomerLeft; //Call when the customer's timer runs out without getting an order
    public static GameEvent onGameOver; //Call when the player runs out of lives
    public static GameEvent onPauseGame;
    public static GameEvent onResumeGame;
    private bool hasGameStarted;

    public void StartGame()
    {
        if(hasGameStarted)
        {
            ResumeGame();
        }
        else
        {
            hasGameStarted = true;
            onStartGame?.Invoke();
            menu.SetActive(false);
            isPaused = false;
            SwitchToDirectInteractors();
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        isPaused = true;
        onPauseGame?.Invoke();
        menu.SetActive(true);
        SwitchToRayInteractors();
        if(hasGameStarted)pausedText.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        onResumeGame?.Invoke();
        menu.SetActive(false);
        SwitchToDirectInteractors();
        pausedText.SetActive(false);
    }

    private void OnToggleMenu()
    {
        if(!hasGameStarted) return;
        if(isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void SwitchToRayInteractors()
    {
        leftRayInteractor.SetActive(true);
        rightRayInteractor.SetActive(true);

        leftDirectInteractor.SetActive(false);
        rightDirectInteractor.SetActive(false);
    }

    private void SwitchToDirectInteractors()
    {
        leftRayInteractor.SetActive(false);
        rightRayInteractor.SetActive(false);

        leftDirectInteractor.SetActive(true);
        rightDirectInteractor.SetActive(true);
    }

    private void EndGame()
    {
        hasGameStarted = false;
        PauseGame();
    }

    private void OnEnable()
    {
        onGameOver += EndGame;
    }

    private void OnDisable()
    {
        onGameOver -= EndGame;
    }
}
