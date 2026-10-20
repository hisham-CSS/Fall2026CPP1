using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    #region Tunable Parameters
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    #endregion

    #region Ground Check
    //public Transform groundCheckPos;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;
    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = bc.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }
    #endregion

    #region Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Animator anim;
    #endregion

    #region Properties
    private int currentJumps = 0;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded only when falling or stationary
        if (rb.linearVelocityY <= 0f)
        {
            isGrounded = Physics2D.OverlapCircle(CalculateGroundCheckPos(), groundCheckRadius, groundLayer);
        }

        //collect inputs for current frame
        //bool upPressed = Input.GetAxisRaw("Vertical") > 0f;
        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPresssed = Input.GetButtonDown("Jump");


        //apply mechanics based on inputs
        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPresssed && currentJumps < maxJumps)
        {
            currentJumps++;
            isGrounded = false;
            rb.linearVelocityY = 0f;
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }

        if (moveX != 0f) SpriteFlip(moveX);
        if (isGrounded) currentJumps = 0;

        // Update animator parameters
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveX", Mathf.Abs(moveX));

    }

    void SpriteFlip(float moveX) => sr.flipX = (moveX < 0f);
    //{
    //    if (sr.flipX && moveX > 0f || !sr.flipX && moveX< 0f)
    //    {
    //        sr.flipX = !sr.flipX;
    //    }
    //}
}
