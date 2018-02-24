using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public Animator animator;
	public float speed;
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("Speed",speed);
	}
}
