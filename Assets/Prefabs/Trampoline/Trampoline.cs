using Assets.Scripts.Interactables;
using System.Collections;
using TwitchLib.Api.Models.Helix.Games.GetGames;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public Vector2 JumpForce = new Vector2(0.005f, 0.5f);

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private bool affected;
    private GameObject player;

    private void Update()
    {
        if (affected && player.gameObject.GetComponent<Character>().ForwardSpeed > 0)
            player.transform.position += new Vector3(0.01f, 0, 0);
        if (affected && player.gameObject.GetComponent<Character>().ForwardSpeed < 0)
            player.transform.position -= new Vector3(0.01f, 0, 0);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.enabled = true;

            //collision.gameObject.GetComponent<Character>().UseVelocityCap = false;
            affected = true;
            player = collision.gameObject;
            var rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(JumpForce, ForceMode2D.Impulse);

            

            StartCoroutine(WaitForAnimationStop(collision));

        }
    }

    private IEnumerator WaitForAnimationStop(Collision2D collision)
    {
        yield return new WaitForSeconds(0.4f);
        affected = false;
        //collision.gameObject.GetComponent<Character>().UseVelocityCap = true;
        animator.enabled = false;
    }
}
