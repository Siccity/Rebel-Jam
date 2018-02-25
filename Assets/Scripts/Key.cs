using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool Player1Access;
    public bool Player2Access;

    private Vector3 KeyPosition;
    void Awake(){
        KeyPosition = transform.position;
    }

    public bool HasAccess(int ID)
    {
        return ID == 1 ? Player1Access : (ID == 2 && Player2Access);
    }

    public void ResetKeyPosition(){
        transform.position = KeyPosition;
    }
}
