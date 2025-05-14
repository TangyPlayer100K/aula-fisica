using NUnit.Framework;
using UnityEngine;

public class PieceScoreDetector : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public int pecasDoLayer = 0;
    public Vector3 startPosition;
    public JengaGameManager jengaGameManager;

    [Header("config")]
    public float moveDistanceIncrease;

    [Header("Scores")]
    public float scoreWood = 20f;
    public float scoreMetal = 30f;
    public float scoreIce = 40f;
    public float scoreRubber = 50f;

    public GameObject lastPiece;


    void Start()
    {
        startPosition = transform.position;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrou no trigger: " + other.gameObject.name);
        if (other.gameObject != lastPiece) 
        {
            if (other.gameObject.CompareTag("wood"))
            {
                jengaGameManager.AddScore(scoreWood);
                pecasDoLayer++;
                if (pecasDoLayer >= 3)
                {
                    LevantarObjeto(moveDistance);
                }
            }
            lastPiece = other.gameObject;
            if (other.gameObject.CompareTag("metal"))
            {
                jengaGameManager.AddScore(scoreMetal);
                pecasDoLayer++;
                if (pecasDoLayer >= 3)
                {
                    LevantarObjeto(moveDistance);
                }
            }
            lastPiece = other.gameObject;
            if (other.gameObject.CompareTag("ice"))
            {
                jengaGameManager.AddScore(scoreIce);
                pecasDoLayer++;
                if (pecasDoLayer >= 3)
                {
                    LevantarObjeto(moveDistance);
                }
            }
            lastPiece = other.gameObject;
            if (other.gameObject.CompareTag("rubber"))
            {
                jengaGameManager.AddScore(scoreRubber);
                pecasDoLayer++;
                if (pecasDoLayer >= 3)
                {
                    LevantarObjeto(moveDistance);
                }
            }
            lastPiece = other.gameObject;
        }
        
    }

    public void LevantarObjeto(float distancia)
    {
        pecasDoLayer = 0;
        startPosition += Vector3.up * distancia;
        transform.position = startPosition;
        moveDistance += moveDistanceIncrease;
    }
}
