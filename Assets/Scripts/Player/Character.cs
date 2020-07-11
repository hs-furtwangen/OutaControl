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

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
            GameLogic.State = GameState.GameOver;

        // TODO: Refactor. Make it good :)
        var vec = (ForwardDirection == MovingDirection.Right ? 1.0f : -1.0f) * ForwardSpeed * Time.deltaTime;
        transform.position += Vector3.right * vec;

        //++positionCheckCnt;

        //// check every x frames (user set) if we kept moving, if not jump
        //if ((positionCheckCnt * Time.deltaTime) > FrameCntAfterToCheckForStuck)
        //{
        //    if (transform.position.x - lastPosition.x < ForwardSpeed * 5f)
        //    {
        //        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400f);
        //    }

        //    lastPosition = transform.position;
        //    positionCheckCnt = 0;
        //}

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
