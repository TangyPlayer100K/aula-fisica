using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JengaGameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int totalPlayers = 2; 
    public List<float> playerScores = new List<float>(); 
    public int currentPlayer = 0; 
    public int fallenPiecesCount = 0; 
    public int piecesToLose = 2; 

    public TMP_Text currentPlayerText; 
    public TMP_Text playerScoreText; 
    public TMP_Text fallenPiecesText; 
    public Button endTurnButton; 

    public TMP_Text gameoverText;
    public GameObject gameoverGameObject;
    public bool gameIsOver;

    private void Start()
    {
        gameIsOver = false;
        for (int i = 0; i < totalPlayers; i++)
        {
            playerScores.Add(0);
        }

        UpdateUI();

        endTurnButton.onClick.AddListener(PassTurn);
    }

    void UpdateUI()
    {
        currentPlayerText.text = "Jogador " + (currentPlayer + 1);
        playerScoreText.text = "Pontuação: " + playerScores[currentPlayer];
        fallenPiecesText.text = "Peças Caídas: " + fallenPiecesCount;
    }

    void PassTurn()
    {
        if (fallenPiecesCount >= piecesToLose)
        {
            LoseGame();
            return;
        }

        currentPlayer = (currentPlayer + 1) % totalPlayers;

        fallenPiecesCount = 0;

        UpdateUI();
    }

    public void PieceFallen()
    {
        fallenPiecesCount++;

        if (fallenPiecesCount >= piecesToLose)
        {
            LoseGame();
        }
        else
        {
            UpdateUI();
        }
    }

    public void AddScore(float score)
    {
        playerScores[currentPlayer] += score + 10f;
        fallenPiecesCount = 0;
        UpdateUI();
        CheckWin();
    }

    void CheckWin()
    {
        if (playerScores[currentPlayer] >= 200f)
        {
            Debug.Log("Jogador " + (currentPlayer + 1) + " venceu o jogo!");
            gameoverText.text = (currentPlayer + 1) + " venceu o jogo!";
            gameoverGameObject.SetActive(true);
            gameIsOver = true;
        }
    }

    void LoseGame()
    {
        if(gameIsOver == false)
        {
            Debug.Log("Jogador " + (currentPlayer + 1) + " perdeu o jogo!");
            gameoverText.text = (currentPlayer + 1) + " perdeu o jogo!";
            gameoverGameObject.SetActive(true);
        }
    }
}
