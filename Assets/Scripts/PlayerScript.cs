using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpPower = 10.0f;
    public bool isGrounded = false;
    Rigidbody2D myRigidbody;
    // Start is called before the first frame update
    void Start() {
        myRigidbody = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(Input.GetKey(KeyCode.Space) && isGrounded) {
            myRigidbody.AddForce(
                Vector3.up 
                * jumpPower * myRigidbody.mass 
                * myRigidbody.gravityScale * 20.0f
            ); 
        }
    }

    void OnCollisionEnter2D (Collision2D other) {
        if(other.collider.tag == "Ground"){
            isGrounded = true;
        }
    }
    
    void OnCollisionStay2D (Collision2D other) {
        if(other.collider.tag == "Ground"){
            isGrounded = true;
        }
    }

    void OnCollisionExit2D (Collision2D other) {
        if(other.collider.tag == "Ground"){
            isGrounded = false;
        }
    }
}
