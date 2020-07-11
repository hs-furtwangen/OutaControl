using UnityEngine;

public enum MovingDirection
{
    Right = 0,
    Left
}

public class Character : MonoBehaviour
{
    public int Health = 5;
    public float InvulnerabilityDuration = 5.0f;
    public float ForwardSpeed = 1.0f;

    private MovingDirection _forwardDirection = MovingDirection.Right;

    public MovingDirection ForwardDirection
    {
        get => _forwardDirection;
        set
        {
            _forwardDirection = value;
            GetComponent<SpriteRenderer>().flipX = _forwardDirection == MovingDirection.Left;           
        }
    }



    public int FrameCntAfterToCheckForStuck = 5;
    public float MaxAllowedZAngleInDeg = 35.0f;

    private Vector3 lastPosition;
    private int positionCheckCnt;
    private float invulnerabilityCooldown = 0.0f;

    private Animator animator;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();


        animator.SetBool("IsFalling", true);
        lastPosition = transform.position;
  

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsFalling", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("IsFalling", false);
        animator.SetBool("IsIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
            GameLogic.State = GameState.GameOver;

        // TODO: Refactor. Make it good :)

        ++positionCheckCnt;
        if(positionCheckCnt > 1000 && !animator.GetBool("IsFalling"))
        {
            animator.SetBool("IsWalking", true);

            var vec = (ForwardDirection == MovingDirection.Right ? 1.0f : -1.0f) * ForwardSpeed * Time.deltaTime;
            transform.position += Vector3.right * vec;
        }
     

        //update invulnerability value
        if (invulnerabilityCooldown <= 0)
        {
            invulnerabilityCooldown -= Time.deltaTime;
        }

    }
    public void DealDamage(int amount)
    {
        //check invulnerability
        if (invulnerabilityCooldown <= 0)
        {
            Health -= amount;
            invulnerabilityCooldown = InvulnerabilityDuration;
            //TODO CHECKPOINT IF DEAD
        }
    }
}
