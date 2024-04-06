using System.Collections;
using UnityEngine;

public class SMScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    private Animator anim;
    private SpriteRenderer sprite;
    public int BoxLayer;
    public CapsuleCollider2D weaponCollider;

    private float dirX = 0;
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float jumpForce = 12;
    [SerializeField] private float weaponSpeed = 7;



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
        Vector2 offset = weaponCollider.offset;
        if(dirX>0){
            if(offset.x >=0){
                offset.x += weaponSpeed;
                offset.y += weaponSpeed/3;
            }
            else{
                offset.x *= -1;
                offset.x -= weaponSpeed;
                offset.y += weaponSpeed/3;
            }
        }
        else{
            if(offset.x < 0){

            
                offset.x += weaponSpeed;
                offset.y += weaponSpeed/3;
            }
            else{
                offset.x *= -1;
                offset.x -= weaponSpeed;
                offset.y += weaponSpeed/3;
            }
        }

        yield return new WaitForSeconds(0.5f);

        attacking = false;
        anim.ResetTrigger("attack");

    }




}