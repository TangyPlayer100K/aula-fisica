using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa o namespace do TextMesh Pro

public class JengaGameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int totalPlayers = 2; // N�mero total de jogadores
    public List<int> playerScores = new List<int>(); // Lista de pontua��o dos jogadores
    public int currentPlayer = 0; // Jogador atual
    public int fallenPiecesCount = 0; // N�mero de pe�as ca�das
    public int piecesToLose = 2; // N�mero de pe�as que faz o jogador perder
    public TMP_Text currentPlayerText; // Exibi��o do jogador atual (TextMesh Pro)
    public TMP_Text playerScoreText; // Exibi��o da pontua��o do jogador atual (TextMesh Pro)
    public TMP_Text fallenPiecesText; // Exibi��o do n�mero de pe�as ca�das (TextMesh Pro)
    public Button endTurnButton; // Bot�o para passar o turno

    [Header("Piece Settings")]
    public List<int> pieceScores; // Pontua��o de cada tipo de pe�a (ordem de 0 a 3 para os tipos de pe�a)

    public TMP_Text gameoverText;
    public GameObject gameoverGameObject;

    // Inicializa o jogo
    private void Start()
    {
        // Inicia a pontua��o dos jogadores
        for (int i = 0; i < totalPlayers; i++)
        {
            playerScores.Add(0); // Pontua��o inicial para cada jogador
        }

        // Atualiza a UI
        UpdateUI();

        // Adiciona a fun��o de passar o turno ao bot�o
        endTurnButton.onClick.AddListener(PassTurn);
    }

    // Atualiza a UI com informa��es sobre o jogador atual e suas pontua��es
    void UpdateUI()
    {
        currentPlayerText.text = "Jogador " + (currentPlayer + 1);
        playerScoreText.text = "Pontua��o: " + playerScores[currentPlayer];
        fallenPiecesText.text = "Pe�as Ca�das: " + fallenPiecesCount;
    }

    // Passa o turno para o pr�ximo jogador
    void PassTurn()
    {
        // Verifica se o jogador perdeu (se mais de uma pe�a caiu)
        if (fallenPiecesCount >= piecesToLose)
        {
            LoseGame();
            return;
        }

        // Incrementa o turno
        currentPlayer = (currentPlayer + 1) % totalPlayers; // Alterna entre jogadores (ex: de 0 vai para 1, de 1 vai para 0)

        // Atualiza a UI
        UpdateUI();
    }

    // Quando uma pe�a cair, verificamos quantas ca�ram
    public void PieceFallen()
    {
        fallenPiecesCount++;

        // Verifica se o jogador perdeu
        if (fallenPiecesCount >= piecesToLose)
        {
            LoseGame();
        }
        else
        {
            // Atualiza a UI
            UpdateUI();
        }
    }

    // Quando uma pe�a � colocada na torre, d� pontos ao jogador
    public void PiecePlaced(int pieceType)
    {
        // Garante que o tipo de pe�a seja v�lido
        if (pieceType >= 0 && pieceType < pieceScores.Count)
        {
            // Adiciona os pontos baseados no tipo de pe�a
            playerScores[currentPlayer] += pieceScores[pieceType];

            // Atualiza a UI
            UpdateUI();
        }
    }

    // Fun��o que � chamada quando o jogador perde
    void LoseGame()
    {
        // Exibe mensagem de perda ou qualquer outra l�gica de fim de jogo
        Debug.Log("Jogador " + (currentPlayer + 1) + " perdeu o jogo!");
        gameoverText.text = (currentPlayer + 1) + " perdeu o jogo!";
        gameoverGameObject.SetActive(true);
    }
}
