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
    private bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
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
        else if (attack)
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
        attack = true;
        anim.SetTrigger("attack");

        // Wait for a short duration (adjust as needed)
        yield return new WaitForSeconds(0.5f);

        // Reset attack state
        attack = false;
        anim.ResetTrigger("attack");
    }
}
