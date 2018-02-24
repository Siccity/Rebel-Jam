using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public Animator Animator;
	public float Speed;

    public float MovementSpeed;
    public CharacterController CharacterControlerVariable;

    // Update is called once per frame
    void Update() {
        Animator.SetFloat("Speed", Speed);

    }
    void FixedUpdate(){

        float moveHorizontal = - Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0.0F, moveHorizontal);
        Speed = movement.magnitude;
        //CharacterControlerVariable.Move(movement * Time.deltaTime*MovementSpeed);
        if (movement != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(movement);
        }

        CharacterControlerVariable.Move(movement / 20);

        //Check if we touch the ground.
        Vector3 transformDown = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, transformDown, 0))
            print("We are falling");
            Vector3 fall = new Vector3(0.0f, -0.1f, 0.0f);
            CharacterControlerVariable.Move(fall);
    }
}
