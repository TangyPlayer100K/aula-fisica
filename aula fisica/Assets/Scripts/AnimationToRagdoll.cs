using System.Collections;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    [SerializeField] float respawnTime = 30f;
    Rigidbody[] rigidbodies;
    bool bIsRagdoll = false;

    void Start()
    {
        Debug.Log($"Colisão detectada com:");

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Colisão detectada com: " + $"{collision.gameObject.name}" + $"- tag: {collision.gameObject.tag} - bIsRagdoll: " + $"{bIsRagdoll}");

        // só ativa ragdoll se AINDA estiver animando
        if (collision.gameObject.CompareTag("Projectile") && !bIsRagdoll)
        {
            Debug.Log("Colidiu com projetil!");
            ToggleRagdoll(false);
            StartCoroutine(GetBackUp());
        }
    }

    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(true);
    }

    private void ToggleRagdoll(bool bisAnimating)
    {
        bIsRagdoll = !bisAnimating;
        myCollider.enabled = bisAnimating;

        foreach (Rigidbody ragdollBone in rigidbodies)
        {
            Debug.Log($"{ragdollBone.name} isKinematic = {ragdollBone.isKinematic}");
        }

        foreach (Rigidbody ragdollBone in rigidbodies)
        {
            ragdollBone.isKinematic = bisAnimating;
        }

        GetComponent<Animator>().enabled = bisAnimating;
        if (bisAnimating)
        {
            RandomAnimation();
        }
    }

    void RandomAnimation()
    {
        int randomNum = UnityEngine.Random.Range(0, 2);
        Debug.Log(randomNum);
        Animator animator = GetComponent<Animator>();

        if (randomNum == 0)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

    }
}
