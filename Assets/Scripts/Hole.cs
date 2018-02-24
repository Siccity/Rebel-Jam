using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {

    public bool FellInHole = false;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Something fell in hole");
        if(col.gameObject.tag == "Crate" && !FellInHole){
            Debug.Log("Crate fell in hole");
            FellInHole = true;
            StartCoroutine(Fall(col.gameObject));
        }
        else if (col.gameObject.tag == "Player" && !FellInHole)
        {
            Debug.Log("Player Fell in hole");
            FellInHole = true;
            StartCoroutine(Fall(col.gameObject));
            //FellInHole = false;

        }
    }

    private void OnTriggerExit(Collider col)
    {
        FellInHole = false;
    }

    IEnumerator Fall(GameObject fallingObject){
        fallingObject.transform.position = new Vector3(transform.position.x-0.25f, fallingObject.transform.position.y, transform.position.z);
        for (float f = 1f; f >= 0; f -= Time.deltaTime)
        {
            Debug.Log("counter: "+f);

            fallingObject.transform.position = new Vector3(transform.position.x - 0.25f, fallingObject.transform.position.y-5*Time.deltaTime, transform.position.z);

            yield return null;
        }

    }
}
