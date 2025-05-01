using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraOrbit : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -10);

    [Header("Rotation Settings")]
    public float rotationSpeed = 5f;
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 30f;

    private float currentDistance;

    private void Start()
    {
        if (target == null)
        {
            GameObject pivot = new GameObject("Camera Pivot");
            pivot.transform.position = Vector3.zero;
            target = pivot.transform;
        }

        currentDistance = offset.magnitude;
        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        HandleRotation();
        HandleZoom();
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // botão direito do mouse
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed;

            Quaternion camTurnAngle = Quaternion.Euler(mouseY, mouseX, 0);
            offset = camTurnAngle * offset;
        }

        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    void HandleZoom()
    {
        Debug.Log("toco o zoom");
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minZoom, maxZoom);

        offset = offset.normalized * currentDistance;
    }
}
