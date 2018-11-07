using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    public float runSpeed = 4f;

    private Rigidbody2D myRigidBody;
	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Run();
        
	}

    private void Run() {

        float horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal");//* runSpeed) * Time.deltaTime;

        Vector2 playerVelocity = new Vector2(horizontalMove *runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        //transform.position = new Vector2(transform.position.x + horizontalMove, transform.position.y);
        FlipSprite();
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) >Mathf.Epsilon;//If the player is moving horizontally
        if(playerHasHorizontalSpeed)
        {
            //reverse current x-axis scaling
            transform.localScale = new Vector3(Mathf.Sign(myRigidBody.velocity.x),1,1);
            
        }

    }
    
}
