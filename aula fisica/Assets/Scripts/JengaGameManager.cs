using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa o namespace do TextMesh Pro

public class JengaGameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int totalPlayers = 2; // Número total de jogadores
    public List<int> playerScores = new List<int>(); // Lista de pontuação dos jogadores
    public int currentPlayer = 0; // Jogador atual
    public int fallenPiecesCount = 0; // Número de peças caídas
    public int piecesToLose = 2; // Número de peças que faz o jogador perder
    public TMP_Text currentPlayerText; // Exibição do jogador atual (TextMesh Pro)
    public TMP_Text playerScoreText; // Exibição da pontuação do jogador atual (TextMesh Pro)
    public TMP_Text fallenPiecesText; // Exibição do número de peças caídas (TextMesh Pro)
    public Button endTurnButton; // Botão para passar o turno

    [Header("Piece Settings")]
    public List<int> pieceScores; // Pontuação de cada tipo de peça (ordem de 0 a 3 para os tipos de peça)

    public TMP_Text gameoverText;
    public GameObject gameoverGameObject;

    // Inicializa o jogo
    private void Start()
    {
        // Inicia a pontuação dos jogadores
        for (int i = 0; i < totalPlayers; i++)
        {
            playerScores.Add(0); // Pontuação inicial para cada jogador
        }

        // Atualiza a UI
        UpdateUI();

        // Adiciona a função de passar o turno ao botão
        endTurnButton.onClick.AddListener(PassTurn);
    }

    // Atualiza a UI com informações sobre o jogador atual e suas pontuações
    void UpdateUI()
    {
        currentPlayerText.text = "Jogador " + (currentPlayer + 1);
        playerScoreText.text = "Pontuação: " + playerScores[currentPlayer];
        fallenPiecesText.text = "Peças Caídas: " + fallenPiecesCount;
    }

    // Passa o turno para o próximo jogador
    void PassTurn()
    {
        // Verifica se o jogador perdeu (se mais de uma peça caiu)
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

    // Quando uma peça cair, verificamos quantas caíram
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

    // Quando uma peça é colocada na torre, dá pontos ao jogador
    public void PiecePlaced(int pieceType)
    {
        // Garante que o tipo de peça seja válido
        if (pieceType >= 0 && pieceType < pieceScores.Count)
        {
            // Adiciona os pontos baseados no tipo de peça
            playerScores[currentPlayer] += pieceScores[pieceType];

            // Atualiza a UI
            UpdateUI();
        }
    }

    // Função que é chamada quando o jogador perde
    void LoseGame()
    {
        // Exibe mensagem de perda ou qualquer outra lógica de fim de jogo
        Debug.Log("Jogador " + (currentPlayer + 1) + " perdeu o jogo!");
        gameoverText.text = (currentPlayer + 1) + " perdeu o jogo!";
        gameoverGameObject.SetActive(true);
    }
}
