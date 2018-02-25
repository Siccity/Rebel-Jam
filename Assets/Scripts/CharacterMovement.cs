using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour{

    public GameObject HandHeldKey;

    private Vector3 StartPosition = new Vector3(-4.2f, 0f, -2f);


    public Animator Animator;
	public float Speed;

    public bool HasKey = false;
    public GameObject KeyObject;

    private float MovementSpeed = 3;
    public CharacterController CharacterControlerVariable;

    public string HorizontalAxis;
    public string VerticalAxis;
    public string UseButton;
    public LayerMask Mask;
    public LayerMask MirrorMask;
    private bool normalMask = true;
    public int NormalLayer;
    public int MirrorLayer;
    public int PlayerID;

    public GameObject Body;
    public GameObject MirrorBody;


    private bool isUsingCrate = false;
    private Crate crate;
    private Vector3 direction;

    private LevelManager levelManager;
    private ScoreManager scoreManager;

    void Awake()
    {
        //scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        //levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update() {
        Animator.SetFloat("Speed", Speed);

    }

    void FixedUpdate(){

        float moveHorizontal = - Input.GetAxis(HorizontalAxis);
        float moveVertical = Input.GetAxis(VerticalAxis);

        Vector3 movement = new Vector3(moveVertical, 0.0F, normalMask?moveHorizontal:-moveHorizontal);


        if (Input.GetButtonDown(UseButton))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + (Vector3.up * 0.5f), transform.forward, out hit, 0.7f))
            {
                if (hit.transform.gameObject.tag == "Player" && !HasKey)
                {
                    Debug.Log("Player Hit");
                    if (HasKey)
                    {
                        ThrowKey();
                    }

                    Animator.SetTrigger("Hit");
                    hit.transform.gameObject.GetComponent<CharacterMovement>().Hit(transform.forward);

                }
            }
            else if (Physics.Raycast(transform.position + (Vector3.up * 0.5f), transform.forward, out hit, 0.7f, normalMask?Mask:MirrorMask)){
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
                    Key key = hit.transform.gameObject.GetComponent<Key>();

                    if (key.HasAccess(PlayerID))
                    {
                        HasKey = true;
                        KeyObject = hit.transform.gameObject;
                        KeyObject.SetActive(false);
                        HandHeldKey.SetActive(true);
                        Debug.Log("found Key");
                    }
                }
                else if (hit.transform.gameObject.tag == "Door")
                {
                    Debug.Log("Det er en dør");
                    if (HasKey)
                    {
                        Debug.Log("Du vandt, sejt!");
                        if (PlayerID == 1)
                        {
                            scoreManager.IncreasePlayer1Score();
                        }
                        else if(PlayerID == 2)
                        {
                            scoreManager.IncreasePlayer2Score();                            
                        }
                        
                        ResetCharacterOnLevelLoad();

                        levelManager.NextLevel();
                        
                    }
                }
                else if (hit.transform.gameObject.tag == "GateFence" && !HasKey){
                    Debug.Log("Der er en låge");
                    Animator.SetBool("OpenDoor",true);
                    hit.transform.gameObject.GetComponent<Animator>().SetBool("Open", true);
                    
                }
                else if (hit.transform.gameObject.CompareTag("WaterPump")) {
                    WaterPump wp = hit.transform.gameObject.GetComponent<WaterPump>();
                    if (wp != null) wp.Interact();
                    else Debug.LogWarning("mangler WaterPump component!");
                }
                else if (hit.transform.gameObject.tag == "GateFenceMirror" && !HasKey)
                {
                    Debug.Log("Der er en mirror portal");
                    Animator.SetBool("OpenDoor", true);
                    //mirror player with shadow
                    if (normalMask)
                    {
                        gameObject.layer = MirrorLayer;
                        Body.layer = MirrorLayer;
                        MirrorBody.layer = NormalLayer;
                    }
                    else
                    {
                        gameObject.layer = NormalLayer;
                        Body.layer = NormalLayer;
                        MirrorBody.layer = MirrorLayer;
                    }
                    normalMask = !normalMask;
                }
                else
                {
                    Taunt();
                }
            }
            else if (HasKey)
            {
                ThrowKey();
            }
            else
            {
                Taunt();
            }
        }
        else if(Input.GetButtonUp(UseButton)){
                isUsingCrate = false;
        }


        if (isUsingCrate)
        {
            var diff = 0f;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                movement = new Vector3(movement.x, 0f, 0f);
                diff = movement.x;
            }
            else
            {
                movement = new Vector3(0f, 0f, movement.z);
                diff = movement.z;
            }

            Speed = diff/2;

            crate.Move(movement * (MovementSpeed/2));
            CharacterControlerVariable.Move(movement * Time.deltaTime * (MovementSpeed/2));
            Animator.SetBool("Pushing", true);
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
            Animator.SetBool("Pushing", false);
        }

        //Check if we touch the ground.
        Vector3 transformDown = transform.TransformDirection(Vector3.down);
        if (!Physics.Raycast(transform.position, transformDown, 0.05f))
        {
            //print("We are falling");
            Vector3 fall = new Vector3(0.0f, -0.1f, 0.0f);
            CharacterControlerVariable.Move(fall);
        }

        if (transform.position.y < -4f)
        {
            ResetCharacterOnLevelLoad();
        }
    }

    private void Taunt()
    {
        Animator.SetTrigger("Taunt");
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Instakill");
        if (col.CompareTag("Instakill")) {
            ResetPosition();
        }
    }

    private void ThrowKey()
    {
        HasKey = false;
        KeyObject.SetActive(true);
        HandHeldKey.SetActive(false);
        KeyObject.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    public void Hit(Vector3 hitDirection)
    {
        if (HasKey)
        {
            ThrowKey();
        }

        StartCoroutine(Knockback(hitDirection));
    }

    IEnumerator Knockback(Vector3 hitDirection)
    {
        for (float i = 0.2f; i > 0; i -= Time.deltaTime)
        {
            CharacterControlerVariable.Move(hitDirection*Time.deltaTime * 4);
            yield return null;
        }

        
    }

    private void ResetCharacterOnLevelLoad(){
        ResetPosition();
        if (KeyObject != null && HasKey){
            HandHeldKey.SetActive(false);
            HasKey = false;
            KeyObject.GetComponent<Key>().ResetKeyPosition();
            KeyObject.SetActive(true);
        }
    }

    private void ResetPosition(){
        transform.position = StartPosition;

        //trying to reset rotation

        //CharacterControlerVariable.enabled = true;
        //Vector3 resetRotation = new Vector3(1, 0.0F, 0);
        //CharacterControlerVariable.Move(resetRotation* Time.deltaTime * MovementSpeed);
        //CharacterControlerVariable.enabled = false;
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 0.7f), Color.cyan);
    }
}
