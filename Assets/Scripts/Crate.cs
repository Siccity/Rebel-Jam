﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    public CharacterController Controller;
    public CharacterController CloneController;

    public bool DisableClone = false;

	void Start () {

	}
	
	void FixedUpdate () 
    {
        //Check if we touch the ground.
        Vector3 transformDown = transform.TransformDirection(Vector3.down);
        if (!Physics.Raycast(transform.position, transformDown, 0.5f)){
            //print("We are falling");
            Vector3 fall = new Vector3(0.0f, -0.1f, 0.0f);
            Controller.Move(fall);
        }
    }

    public void Move(Vector3 movement){
        Controller.Move(movement*Time.deltaTime);
        if (DisableClone)
        {
            return;
        }
        CloneController.Move(movement*Time.deltaTime);
    }
}
