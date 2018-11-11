using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Config
    [SerializeField]
    float runSpeed = 4f;
    [SerializeField]
    float jumpSpeed = 10f;
    [SerializeField]
    float climbSpeed = 1f;
    [SerializeField]
    Vector2 dieKnockback = new Vector2(10, 10);
    // State
    bool isAlive = true;


    //private cache values
    //jumped in midair bool
    //walljumped bool
    private float gravityScaleAtStart;
    // Cached component references
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private Collider2D myCollider;
    private Collider2D myFeet;


    // Use this for initialization
    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update() {
        if (isAlive) {
            Run();
            FlipSprite();
            Jump();
            ClimbLadder();
            Death();
        }


    }

    private void Death() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("Enemy"))
            || myCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("Hazards"))
            ) {

            isAlive = false;
            myAnimator.SetTrigger("Death");
            myRigidBody.velocity = new Vector2(dieKnockback.x * Mathf.Sign(myRigidBody.velocity.x), dieKnockback.y);
            var gameSession =FindObjectOfType<GameSession>();

            gameSession.ProcessPlayerDeath();
        }






    }

    private void Run() {

        float horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal");//* runSpeed) * Time.deltaTime;

        Vector2 playerVelocity = new Vector2(horizontalMove * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        //transform.position = new Vector2(transform.position.x + horizontalMove, transform.position.y);


        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;//If the player is moving horizontally
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

    }

    private void Jump() {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;//If the player is moving horizontally
        if (playerHasHorizontalSpeed) {
            //reverse current x-axis scaling
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);

        }

    }

    private void ClimbLadder() {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }


        float verticalMove = CrossPlatformInputManager.GetAxis("Vertical");
        bool verticalDirection = Mathf.Abs(verticalMove) > Mathf.Epsilon;
        Debug.Log("vertical Dir:2" + verticalDirection);
        if (verticalDirection) {
            myRigidBody.gravityScale = 0f;
            myAnimator.SetBool("Climbing", true);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x / climbSpeed, verticalMove * climbSpeed);

        } else {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = 0f;
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);

        }




    }

    private void OnTriggerEnter2D(Collider2D collision) {

        //myAnimator.SetBool("Climbing", true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //myAnimator.SetBool("Climbing", false);
    }












    /*
     
     
     using UnityEngine;
 
public class PlayerController : MonoBehaviour {
    public float speed = 3f;
    public float jumpForce = 12f;
    public float firstJumpForce = 8f;
    public int jumps = 2;
 
    private int remainingJumps;
    private float x;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private CapsuleCollider2D bodyCollider;
    private BoxCollider2D feetCollider;
 
    // Use this for initialization
    void Awake () {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }
    
    // Update is called once per frame
    void Update () {
        Run(Input.GetAxis("Horizontal"));
    }
    void FixedUpdate() {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("World"))) {
            remainingJumps = jumps;
        }
 
        if(Input.GetButtonDown("Jump")) {
            Jump();
        }
    }
    private void Jump() {
        if((feetCollider.IsTouchingLayers(LayerMask.GetMask("World")) || remainingJumps > 0)) {
            if(remainingJumps == jumps) {
                rigidbody.AddForce(Vector2.up * firstJumpForce, ForceMode2D.Impulse);
            } else {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            remainingJumps--;
        }
    }
    private void Run(float x) {
        if (x == 0) {
            animator.SetBool("isRunning", false);
            return;
        }
        animator.SetBool("isRunning", true);
        transform.Translate(Vector2.right * x * speed * Time.deltaTime);
        transform.localScale = new Vector2(Mathf.Sign(x),1);
    }
}
     
     */

}
