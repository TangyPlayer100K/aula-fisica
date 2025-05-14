using UnityEngine;

public class JengaPieceFallDetector : MonoBehaviour
{
    private JengaGameManager gameManager; // Referência ao JengaGameManager

    // Detecta quando a peça toca o chão ou cai da torre
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a colisão foi significativa o suficiente para considerarmos a peça como caída
        if (collision.gameObject.CompareTag("chao")) // Velocidade relativa suficiente para a peça cair
        {
            // Tenta encontrar o JengaGameManager na cena
            if (gameManager == null)
            {
                gameManager = FindObjectOfType<JengaGameManager>(); // Busca o GameManager na cena
            }

            // Se o GameManager foi encontrado, chama a função PieceFallen()
            if (gameManager != null)
            {
                gameManager.PieceFallen();
            }

            //// Destrói a peça após cair, para garantir que não interaja mais
            //Destroy(gameObject);
        }
    }
}
