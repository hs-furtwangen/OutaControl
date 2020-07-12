using Assets.Scripts.Interactables;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public Vector2 JumpForce = new Vector2(0.005f, 0.5f);

    private Animator animator;

    new void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.enabled = true;

            collision.gameObject.GetComponent<Character>().UseVelocityCap = false;

            var rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(JumpForce, ForceMode2D.Impulse);

            collision.gameObject.GetComponent<Character>().UseVelocityCap = true;

            StartCoroutine(WaitForAnimationStop());

        }
    }

    private IEnumerator WaitForAnimationStop()
    {
        yield return new WaitForSeconds(0.4f);
        animator.enabled = false;
    }
}
