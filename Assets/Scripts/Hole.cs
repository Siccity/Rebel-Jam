using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    public Animator Animator;

    private bool animationPlaying;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Something fell in hole");
        if (col.gameObject.tag == "Crate" && !animationPlaying)
        {
            Debug.Log("Crate fell in hole");
            animationPlaying = true;
            Animator.SetBool("Crate", true);
            col.gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Player" && !animationPlaying)
        {
            Debug.Log("Player Fell in hole");
            animationPlaying = true;
            //col.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            col.gameObject.GetComponent<Animator>().SetTrigger("Fall");

            Invoke("ResetTrigger", 1f);
        }
    }

    private void ResetTrigger()
    {
        animationPlaying = false;
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
