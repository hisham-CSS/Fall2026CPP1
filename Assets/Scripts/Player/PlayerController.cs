using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    #region Tunable Parameters
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    #endregion

    #region Ground Check Parameters
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public bool isGrounded;
    #endregion

    #region Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocityY <= 0f)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPresssed = Input.GetButtonDown("Jump");

        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPresssed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (moveX != 0f) SpriteFlip(moveX);
    }

    void SpriteFlip(float moveX) => sr.flipX = (moveX < 0f);
    //{
    //    if (sr.flipX && moveX > 0f || !sr.flipX && moveX< 0f)
    //    {
    //        sr.flipX = !sr.flipX;
    //    }
    //}
}
