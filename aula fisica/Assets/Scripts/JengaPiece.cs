using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class JengaPiece : MonoBehaviour
{
    [Header("Drag Setting")]
    public float maxDragDistance = 2f;// distnacia maxima que a pe�a pode ser arrastada

    [Header("Push Settings")]
    public float pushForce = 5f; // for�a aplicada em cloque simples
    public float doubleClickForceMultiplier = 2f; // multiplicador de for�a para duplo clique
    public float doubleClickThreshold = 0.3f; // tempo maximo entre cliques para ser considerado duplo clique

    [Header("Keyboard Movement")]
    public float keyboardMoveSpeed = 0.1f; // Velocidade de movimento ao pressionar Q e E

    private Rigidbody rb;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 startDragPosition;
    private float lastClickTime = -1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        // Detecta clique duplo
        float timeSinceLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time;

        if (timeSinceLastClick <= doubleClickThreshold)
        {
            ApplyPush(pushForce * doubleClickForceMultiplier);
            return; // N�o inicia o arrasto se for duplo clique
        }

        // Clique simples aplica a for�a
        ApplyPush(pushForce);

        // Inicia arrasto
        isDragging = true;
        rb.isKinematic = true;

        // Calcula o offset do mouse em rela��o � posi��o da pe�a
        Vector3 mousepos = GetMouseWorldPosition();
        offset = transform.position - mousepos;
        startDragPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        // Obt�m a posi��o do mouse no mundo
        Vector3 mousepos = GetMouseWorldPosition();

        // Calcula a nova posi��o somente no eixo X e Z, mantendo o Y constante
        Vector3 targetPosition = new Vector3(
            mousepos.x + offset.x, // Modifica a posi��o no eixo X com base no arrasto
            transform.position.y,  // Mant�m a posi��o no eixo Y constante
            mousepos.z + offset.z  // Modifica a posi��o no eixo Z com base no arrasto
        );

        transform.position = targetPosition;
    }

    private void OnMouseUp()
    {
        // Libera o arrasto e reativa a f�sica
        isDragging = false;
        rb.isKinematic = false;
    }

    /// <summary>
    /// aplica turva na sla porra da camera como um "empurr�o"
    /// </summary>
    private void ApplyPush(float force)
    {
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }


    /// <summary>
    /// retorna a posi��o do mouse no plano horizontal e altura da pe�a
    /// </summary>
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up , transform.position);// plano horizontal baseado na altura da pe�a
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position;
    }

    private void Update()
    {
        // Verifica se a pe�a est� sendo arrastada antes de permitir o movimento vertical com Q ou E
        if (isDragging)
        {
            // Movimento vertical com as teclas Q e E
            if (Input.GetKey(KeyCode.E)) // Subir
            {
                MoveVertically(keyboardMoveSpeed);
            }
            if (Input.GetKey(KeyCode.Q)) // Descer
            {
                MoveVertically(-keyboardMoveSpeed);
            }
        }
    }

    /// <summary>
    /// Move a pe�a verticalmente com a velocidade especificada, **apenas no eixo Y**.
    /// </summary>
    private void MoveVertically(float moveAmount)
    {
        // A pe�a s� se move no eixo Y, mantendo os outros eixos (X e Z) constantes
        transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
    }



}
