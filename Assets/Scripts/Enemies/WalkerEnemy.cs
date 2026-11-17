using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkerEnemy : BaseEnemy
{
    [SerializeField] private float xVel = 2.0f;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Walk"))
        {
            //if (sr.flipX) rb.linearVelocityX = -xVel;
            //else rb.linearVelocityX = xVel;

            //Ternary operator allows us to write the above if/else statement in one line
            //if we have a simple 
            rb.linearVelocityX = sr.flipX ? -xVel : xVel;
        }
    }

    public override void TakeDamage(int damage, DamageType damageType = DamageType.Default)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Death") || stateInfo.IsName("Squish"))
        {
            // If the enemy is already in the death animation or squish animation, ignore further damage
            return;
        }

        if (damageType == DamageType.JumpedOn)
        {
            anim.SetTrigger("Squish");
            // Optionally, you can add logic to disable the enemy's movement or other behaviors here
            Destroy(transform.parent.gameObject, 0.5f); // Destroy the parent object after a delay to allow the squish animation to play
            return; // Exit the method to prevent further damage processing
        }

        base.TakeDamage(damage, damageType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            anim.SetTrigger("Turn");
            sr.flipX = !sr.flipX;
        }
    }
}
