using UnityEngine;

public class JengaPieceFallDetector : MonoBehaviour
{
    private JengaGameManager gameManager; // Refer�ncia ao JengaGameManager

    // Detecta quando a pe�a toca o ch�o ou cai da torre
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a colis�o foi significativa o suficiente para considerarmos a pe�a como ca�da
        if (collision.gameObject.CompareTag("chao")) // Velocidade relativa suficiente para a pe�a cair
        {
            // Tenta encontrar o JengaGameManager na cena
            if (gameManager == null)
            {
                gameManager = FindObjectOfType<JengaGameManager>(); // Busca o GameManager na cena
            }

            // Se o GameManager foi encontrado, chama a fun��o PieceFallen()
            if (gameManager != null)
            {
                gameManager.PieceFallen();
            }

            //// Destr�i a pe�a ap�s cair, para garantir que n�o interaja mais
            //Destroy(gameObject);
        }
    }
}
