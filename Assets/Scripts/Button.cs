using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour{

    public Boolean ButtonDown = false;

    void OnTriggerEnter(Collider col){
        ButtonDown = true;
        Debug.Log("Button is Down");
    }

    private void OnTriggerExit(Collider col)
    {
        ButtonDown = false;
    }

    // Update is called once per frame
    void Update () {

        //if (ButtonDown)
        //{
        //    //Debug.Log("Button is Down");
        //}
    }
}
