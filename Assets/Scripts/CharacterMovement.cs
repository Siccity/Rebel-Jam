using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	public Animator Animator;
	public float Speed;

    public bool HasKey = false;
    public GameObject KeyObject;

    public float MovementSpeed;
    public CharacterController CharacterControlerVariable;

    public string HorizontalAxis;
    public string VerticalAxis;
    public string UseButton;
    public LayerMask Mask;


    private bool isUsingCrate = false;
    private Crate crate;
    private Vector3 direction;

    // Update is called once per frame
    void Update() {
        Animator.SetFloat("Speed", Speed);

    }

    void FixedUpdate(){

        float moveHorizontal = - Input.GetAxis(HorizontalAxis);
        float moveVertical = Input.GetAxis(VerticalAxis);

        Vector3 movement = new Vector3(moveVertical, 0.0F, moveHorizontal);


        if (Input.GetButtonDown(UseButton))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + (Vector3.up * 0.5f), transform.forward, out hit, 0.7f, Mask))
            {
                Debug.Log(hit.transform.gameObject.name);

                if (hit.transform.gameObject.tag == "Crate" && !HasKey)
                {
                    
                    isUsingCrate = true;
                    Debug.Log("Crate used");

                    direction = transform.forward;
                    crate = hit.transform.gameObject.GetComponent<Crate>();
                   
                }
                else if (hit.transform.gameObject.tag == "Key" && !HasKey)
                {

                    HasKey = true;
                    KeyObject = hit.transform.gameObject;
                    KeyObject.SetActive(false);

                    Debug.Log("found Key");
                }
                else if (hit.transform.gameObject.tag == "Door")
                {
                    Debug.Log("Det er en dør");
                    if (HasKey)
                    {
                        Debug.Log("Du vandt, sejt!");
                    }
                }
            }
            else if (HasKey)
            {
                HasKey = false;
                KeyObject.SetActive(true);
                KeyObject.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            }
        }
        else if(Input.GetButtonUp(UseButton)){
                isUsingCrate = false;
        }

        if (isUsingCrate)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                movement = new Vector3(movement.x, 0f, 0f);
            }
            else
            {
                movement = new Vector3(0f, 0f, movement.z);
            }

            crate.Move(movement * (MovementSpeed/2));
            CharacterControlerVariable.Move(movement * Time.deltaTime * (MovementSpeed/2));
        }
        else
        {
            Speed = movement.magnitude;

            //CharacterControlerVariable.Move(movement * Time.deltaTime*MovementSpeed);
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movement);
            }

            CharacterControlerVariable.Move(movement * Time.deltaTime * MovementSpeed);
        }

        //Check if we touch the ground.
        Vector3 transformDown = transform.TransformDirection(Vector3.down);
        if (!Physics.Raycast(transform.position, transformDown, 0.05f))
        {
            print("We are falling");
            Vector3 fall = new Vector3(0.0f, -0.1f, 0.0f);
            CharacterControlerVariable.Move(fall);
        }
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 0.7f), Color.cyan);
    }
}
