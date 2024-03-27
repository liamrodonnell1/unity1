using System.Collections;
using UnityEngine;

public class SMScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    private Animator anim;
    private SpriteRenderer sprite;

    private float dirX = 0;
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float jumpForce = 12;

    private enum MovementState { idle, running, jumping, attack }
    private bool attacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        dirX = Input.GetAxis("Horizontal");

        myRigidBody.velocity = new Vector2(dirX * moveSpeed, myRigidBody.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(AttackCoroutine());
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        MovementState state;

        if (myRigidBody.velocity.y > .1f || myRigidBody.velocity.y < -.1f)
        {
            state = MovementState.jumping;
        }
        else if (attacking)
        {
            state = MovementState.attack;
        }
        else if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }

    IEnumerator AttackCoroutine()
    {
        attacking = true;
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);

        attacking = false;
        anim.ResetTrigger("attack");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attacking && other.CompareTag("box")) 
        {
            Rigidbody2D otherRigidbody = other.GetComponent<Rigidbody2D>();
            if (otherRigidbody != null)
            {
                Vector2 direction = other.transform.position - transform.position;

                otherRigidbody.AddForce(-direction.normalized * 500f, ForceMode2D.Impulse); 
            }
        }
    }
}