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

        if (collision.collider.CompareTag("Limb"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            transform.SetParent(collision.transform);

            isStuck = true;
        }
    }
}
