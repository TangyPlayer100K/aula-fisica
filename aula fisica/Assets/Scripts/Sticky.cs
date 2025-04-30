using UnityEngine;

public class Sticky : MonoBehaviour
{
    private Rigidbody rb;
    private bool isStuck = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isStuck) return;

        // Verifica se o objeto colidido tem a tag "Limb"
        if (collision.collider.CompareTag("Limb"))
        {
            // Para a física
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            // Gruda no objeto com a tag "Limb"
            transform.SetParent(collision.transform);

            isStuck = true;
        }
    }
}
