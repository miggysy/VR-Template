using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameEvent();
    public static GameEvent onOrderCorrect; //Call when the order submitted was correct
    public static GameEvent onOrderWrong;   //Call when the order submitted was wrong
    public static GameEvent onCustomerLeft; //Call when the customer's timer runs out without getting an order
    public static GameEvent onGameOver; //Call when the player runs out of lives

    
    //FOR TESTING PURPOSES ONLY -------------------------------------------------------------

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }

    private void OnEnable()
    {
        onGameOver += GameOver;
    }

    private void OnDisable()
    {
        onGameOver -= GameOver;
    }
}
