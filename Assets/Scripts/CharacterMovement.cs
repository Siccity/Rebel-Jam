using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public Animator animator;
	public float speed;

    //public float tilt;
    public float movementSpeed;
    public Rigidbody characterRigidbody;

    // Update is called once per frame
    void Update() {
        animator.SetFloat("Speed", speed);

    }
    void FixedUpdate(){

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        characterRigidbody.velocity = movement * movementSpeed;
        speed = characterRigidbody.velocity.magnitude;

        characterRigidbody.position = new Vector3(transform.position.x, 0.0F, transform.position.z);


        //characterRigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, characterRigidbody.velocity.x * -tilt);

    }
}
