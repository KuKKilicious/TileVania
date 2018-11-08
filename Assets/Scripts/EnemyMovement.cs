using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField]
    float moveSpeed = 1f;

    private Rigidbody2D myRigidBody;
    private BoxCollider2D groundCheckCollider;
	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        groundCheckCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {

        myRigidBody.velocity = new Vector2(moveSpeed, 0);
        
	}

    private void TurnAround() {
            moveSpeed *= -1f;
    }

    private void FlipSprite() {
        
            //reverse current x-axis scaling
            transform.localScale = new Vector2(transform.localScale.x*-1f, 1f);
        

    }
    private void OnTriggerExit2D(Collider2D collision) {
        Debug.Log(collision.name+"<TriggerExit");
        //if (collision.name == "Foreground") { //Interaction with other Layers disabled in LayerSettings
        FlipSprite();
        TurnAround();

     //   }
        
    }
}
