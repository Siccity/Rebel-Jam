using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    public CharacterController Controller;
    public CharacterController CloneController;

	void Start () {

	}
	
	void FixedUpdate () 
    {
        var moveDirection = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
	    Controller.Move(moveDirection*Time.deltaTime);
	    CloneController.Move(moveDirection*Time.deltaTime);

        //Check if we touch the ground.
        Vector3 transformDown = transform.TransformDirection(Vector3.down);
        if (!Physics.Raycast(transform.position, transformDown, 0.5f)){
            print("We are falling");
            Vector3 fall = new Vector3(0.0f, -0.1f, 0.0f);
            Controller.Move(fall);
        }
    }

    public void Move(Vector3 movement){
        Controller.Move(movement*Time.deltaTime);
        CloneController.Move(movement*Time.deltaTime);
    }
}