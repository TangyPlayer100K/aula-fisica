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
        // detecta clique duplo
        float timeSinceLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time;

        // se for duplo clique, aplica for�a maior e n�o inicia arrasto
        if (timeSinceLastClick <= doubleClickThreshold)
        {
            ApplyPush(pushForce * doubleClickForceMultiplier);
            return;
        }

        // se for clique simples, apliuca for�a normal
        ApplyPush(pushForce);

        // inicia arrasto
        isDragging = true;
        rb.isKinematic = true;

        // caucula offset entre o mouse e a posi��o atual da pe�a
        Vector3 mousepos = GetMouseWorldPosition();
        offset = transform.position - mousepos;
        startDragPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        // obtem posiu��o do mouse no mundo
        Vector3 mousepos = GetMouseWorldPosition();
        Vector3 targetPos = mousepos + offset;

        // lista a distancia de arrasto
        Vector3 dragVector = targetPos - startDragPosition;
        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        transform.position = startDragPosition + dragVector;
    }

    private void OnMouseUp()
    {
        //  libera arrasto e ativa fisica novamnete
        isDragging = false;
        rb.isKinematic = false;
    }

    /// <summary>
    /// aplica turva na slaporra da camera como um "empurr�o"
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

}
