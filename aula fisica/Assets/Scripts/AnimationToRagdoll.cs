using System.Collections;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    [SerializeField] float respawnTime = 5f;

    Rigidbody[] rigidbodies;
    Animator animator;

    bool bIsRagdoll = false;

    void Start()
    {
        Debug.Log($"Colisão detectada com:");

        animator = GetComponent<Animator>();

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
        animator.SetTrigger("Walk");
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

        animator.SetTrigger("GetUp"); // levanta
        yield return new WaitForSeconds(2f); // tempo da animação de levantar

        animator.SetTrigger("React"); // bate a poeira
        yield return new WaitForSeconds(1f); // tempo da reação

        animator.SetTrigger("Run"); // começa a correr
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

        animator.enabled = bisAnimating;
        //GetComponent<Animator>().enabled = bisAnimating;
        //if (bisAnimating)
        //{
        //    RandomAnimation();
        //}
    }

    //void RandomAnimation()
    //{



    //    int randomNum = UnityEngine.Random.Range(0, 2);
    //    Debug.Log(randomNum);
    //    Animator animator = GetComponent<Animator>();

    //    if (randomNum == 0)
    //    {
    //        animator.SetTrigger("Walk");
    //    }
    //    else
    //    {
    //        animator.SetTrigger("Idle");
    //    }

    //}
}
