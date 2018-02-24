using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    //public GameObject CloneCrate;
    private Vector3 previousPos;
    private Vector3 previousClonePos;

    public CharacterController Controller;
    public CharacterController CloneController;


	void Start () {
        previousPos = transform.position;
        //previousClonePos = CloneCrate.transform.position;
	}
	
	void FixedUpdate () 
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	    Controller.Move(moveDirection*Time.deltaTime);
	    CloneController.Move(moveDirection*Time.deltaTime);

        /*
        if (previousClonePos != CloneCrate.transform.position)
        {
            var diff = CloneCrate.transform.position - previousClonePos;
            transform.Translate(diff);
        }
        */

        previousPos = transform.position;
        //previousClonePos = CloneCrate.transform.position;
	}
}
