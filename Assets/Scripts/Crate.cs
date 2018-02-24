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
        //var moveDirection = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        //Controller.Move(moveDirection*Time.deltaTime);
        //CloneController.Move(moveDirection*Time.deltaTime);
	}

    public void Move(Vector3 movement)
    {
        Controller.Move(movement*Time.deltaTime);
        CloneController.Move(movement*Time.deltaTime);
    }
}
