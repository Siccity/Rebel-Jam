using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public Animator Animator;
    // Use this for initialization
    void Start () {
	}

    
	
	// Update is called once per frame
	void Update () {
	    OpenGate();
    }

    public void OpenGate(){
        transform.Rotate(Vector3.forward * Time.deltaTime*10);
    }
}
