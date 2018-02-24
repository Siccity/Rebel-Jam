using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public Animator animator;
	public float speed;

    public float movementSpeed;
    public CharacterController characterControler;

    // Update is called once per frame
    void Update() {
        animator.SetFloat("Speed", speed);

    }
    void FixedUpdate(){

        float moveHorizontal = - Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0.0F, moveHorizontal);
        characterControler.Move(movement * Time.deltaTime*movementSpeed);

        //characterControler.transform.r

        //Check if we touch the ground.
        Vector3 transformDown = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, transformDown, 1))
            print("We are falling");
            Vector3 fall = new Vector3(0.0f, -0.1f, 0.0f);
            characterControler.Move(fall);
    }
}
