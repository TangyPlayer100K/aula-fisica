using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class JenkaManager : MonoBehaviour
{
    [Header("tower Settings")]
    public int layers = 3;
    public int piecesPerLayer = 3;
    public List<GameObject> piecePrefabs;

    [Header("Piece size Settings")]
    public float pieceSpacing = 0f;
    public float pieceLength = 0.5f;
    public float pieceHeight = 0.3f;

    private void Start()
    {
        BuildTower();
    }

    void BuildTower()
    {
        for (int layerIndex = 0; layerIndex < layers; layerIndex++)
        {
            GenerateLayer(layerIndex);
        }
    }

    void GenerateLayer(int layerIndex)
    {
        Vector3 basePosition = transform.position;
        basePosition.y += layerIndex * (pieceHeight + pieceSpacing);

        bool isOdd = IsOddLayer(layerIndex);
        Quaternion rotation = isOdd ? Quaternion.Euler(0, 90, 0) : Quaternion.identity;
        Vector3 direction = isOdd ? Vector3.right : Vector3.forward;

        // centro da camada: metade do tamnho total ocupado
        float totalWidth = (piecesPerLayer - 1) * (pieceLength + pieceSpacing);
        Vector3 startOffset = -direction * (totalWidth / 2f);

        for (int i = 0; i < piecesPerLayer; i++)
        {
            GameObject prefab = piecePrefabs[Random.Range(0, piecePrefabs.Count)];
            Vector3 offset = direction * i * (pieceLength + pieceSpacing);
            Vector3 spawnPosition = basePosition + startOffset + offset;

            Instantiate(prefab, spawnPosition, rotation);
        }
    }

    bool IsOddLayer(int layerIndex)
    {
        return layerIndex % 2 != 0;
    }
}
