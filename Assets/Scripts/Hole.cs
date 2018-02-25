using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    public Animator AnimatorL;
    public Animator AnimatorR;
    public GameObject PlantL;
    public GameObject PlantR;

    private bool animationLeftPlaying;
    private bool animationRightPlaying;

    private bool leftIn;
    private bool rightIn;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Something fell in hole");
        if (col.gameObject.tag == "Crate")
        {
            Debug.Log("Crate fell in hole");

            if (col.gameObject.layer == 8 && !leftIn)
            {
                AnimatorL.SetTrigger("Crate");
                leftIn = true;
            }
            else if (col.gameObject.layer == 9 && !rightIn)
            {
                AnimatorR.SetTrigger("Crate");
                rightIn = true;
            }
            
            
            col.gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Player")
        {
            if ((!leftIn && col.gameObject.layer == 8))
            {
                Debug.Log("Player Fell in hole");
                animationLeftPlaying = true;
                col.gameObject.GetComponent<Animator>().SetTrigger("Fall");

                //Invoke("ResetTrigger", 1f);
            }
            else if ((!rightIn && col.gameObject.layer == 9))
            {
                Debug.Log("Player Fell in hole");
                animationRightPlaying = true;
                col.gameObject.GetComponent<Animator>().SetTrigger("Fall");

                //Invoke("ResetTrigger", 1f);
            }
        }

    }

    private void ResetTrigger()
    {
        animationLeftPlaying = false;
        animationRightPlaying = false;
    }

    //public bool FellInHole = false;

    //void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("Something fell in hole");
    //    if(col.gameObject.tag == "Crate" && !FellInHole){
    //        Debug.Log("Crate fell in hole");
    //        FellInHole = true;
    //        StartCoroutine(Fall(col.gameObject));
    //    }
    //    else if (col.gameObject.tag == "Player" && !FellInHole)
    //    {
    //        Debug.Log("Player Fell in hole");
    //        FellInHole = true;
    //        StartCoroutine(Fall(col.gameObject));
    //        //FellInHole = false;

    //    }
    //}

    //private void OnTriggerExit(Collider col)
    //{
    //    FellInHole = false;
    //}

    //IEnumerator Fall(GameObject fallingObject){
    //    fallingObject.transform.position = new Vector3(transform.position.x-0.25f, fallingObject.transform.position.y, transform.position.z);
    //    for (float f = 1f; f >= 0; f -= Time.deltaTime)
    //    {
    //        Debug.Log("counter: "+f);

    //        fallingObject.transform.position = new Vector3(transform.position.x - 0.25f, fallingObject.transform.position.y-5*Time.deltaTime, transform.position.z);

    //        yield return null;
    //    }

    //}
}
