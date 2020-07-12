using Assets.Scripts.Interactables;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Trampoline : Movable
{
    public Vector2 JumpForce = new Vector2(0.005f, 0.5f);

    private Animator animator;

    new void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        base.Start();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.enabled = true;

            var rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(JumpForce, ForceMode2D.Impulse);

            StartCoroutine(WaitForAnimationStop());

        }
    }

    private IEnumerator WaitForAnimationStop()
    {

        yield return new WaitForSeconds(0.4f);


        animator.enabled = false;

    }
}
