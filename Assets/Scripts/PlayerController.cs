using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// The distance this player will travel per second.
    [SerializeField]
    private float walkSpeed;
    private float moveSpeed;

    [SerializeField]
    private float rotationSpeed;

    

    /// <summary>
    /// The distance of the raycast length
    /// </summary>
    [SerializeField]
    private float interectionDistance;


    /// The camera attached to the player model should be dragged in from Inspector.
    public Camera playerCamera;
    public GameObject cameraParent;
    public Rigidbody rigibody;
    public Vector3 velocity;

    private string currentState;
    private string nextState;

    public Vector3 playerRotation;
    public Vector3 cameraRotation;

    public GameManager gameManagerScript;
    public Fade fade;

    /// <summary>
    /// Check that the player is on the ground
    /// </summary>
    public bool onGround;
    public float jumpForce;
    private int numOfJump;
    

    // Start is called before the first frame update
    void Start()
    {
        numOfJump = 1;
        nextState = "Idle";

        playerRotation = Vector3.zero;
        rigibody = GetComponent<Rigidbody>();
        velocity = rigibody.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextState != currentState)
        {
            SwitchState();
        }

        Debug.Log(currentState);

        CheckRotation();

        Raycasting();


        //Teleports player back to the platform if they fall off the island
        if (this.transform.position.y < -20)
        {
            Debug.Log("Falling into the void");
            this.transform.position = new Vector3(4, 0, 0);
        }

        //Only when spacebar is pressed and the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            if(numOfJump == 1)
            {
                numOfJump -= 1;
                rigibody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                onGround = false;
            }
            
        }
    }

    void FixedUpdate()
    {
        velocity = rigibody.velocity;

        if (velocity.y < -8)
        {
            Debug.Log("Dummy you falling");
            StartCoroutine(ScreenShake(.15f, .2f));
        }
    }

    //Sets the current state of the player and starts the correct coroutine.
    private void SwitchState()
    {
        StopCoroutine(currentState);

        currentState = nextState;
        StartCoroutine(currentState);
    }

    private IEnumerator Idle()
    {
        while (currentState == "Idle")
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                nextState = "Moving";
            }
            yield return null;
        }
    }

    private IEnumerator Moving()
    {
        while (currentState == "Moving")
        {
            if (CheckMovement())
            {
                nextState = "Idle";
            }
            yield return null;
        }
    }

    private void CheckRotation()
    {
        playerRotation = transform.rotation.eulerAngles;
        playerRotation.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0f, playerRotation.y, 0f);


        cameraRotation = playerCamera.transform.rotation.eulerAngles;
        cameraRotation.x -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }


    /// <summary>
    /// Code for all the raycasting done with the player
    /// </summary>
    void Raycasting()
    {
        //Set the layermask to the quest
        //Only item with the quest will be affected 
        int layerMaskQuest = 1 << LayerMask.NameToLayer("Quest");
        int layerMaskPortal = 1 << LayerMask.NameToLayer("Portal");
        int layerMaskTop = 1 << LayerMask.NameToLayer("QuestTop");
        int layerMaskRitual = 1 << LayerMask.NameToLayer("Ritual");

        //Draw line will show green if hit and red if no hit
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interectionDistance, layerMaskQuest))
        {
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interectionDistance, Color.green);
            gameManagerScript.lookingAtItem = true;

            if (Input.GetKeyDown("e"))
            {
                if (hitInfo.collider.gameObject.name == "Quest Giver")
                {
                    hitInfo.collider.gameObject.GetComponent<QuestGiver>().CheckStage();
                }

                //Checking the object is correct
                //Will change UI to reflect collection
                if (hitInfo.collider.gameObject.tag == "MagicStone")
                {
                    ++gameManagerScript.magicStone;
                    Debug.Log("Collecting magic stone");
                    hitInfo.collider.gameObject.SetActive(false);
                }
                else if (hitInfo.collider.gameObject.tag == "Water")
                {
                    ++gameManagerScript.waterBottle;
                    Debug.Log("Collecting water");
                    hitInfo.collider.gameObject.SetActive(false);
                }
                else if (hitInfo.collider.gameObject.tag == "Flower")
                {
                    ++gameManagerScript.flower;
                    Debug.Log("Collecting flower");
                    hitInfo.collider.gameObject.SetActive(false);
                }
                else if (hitInfo.collider.gameObject.tag == "Pillar")
                {
                    ++gameManagerScript.pillarActivated;
                    Debug.Log("Activating Pillar");
                    hitInfo.collider.gameObject.SetActive(false);
                }
            }
        }
        else if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interectionDistance, layerMaskPortal))
        {
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interectionDistance, Color.green);
            gameManagerScript.lookingAtItem = true;

            if (Input.GetKeyDown("e"))
            {
                if(hitInfo.collider.gameObject.tag == "AreaOne")
                {
                    fade.LocationCordinate(4, 0, -5);
                } 
                else if (hitInfo.collider.gameObject.tag == "AreaTwo")
                {
                    fade.LocationCordinate(-7, 0, -5);
                }
                else if (hitInfo.collider.gameObject.tag == "AreaThree")
                {
                    fade.LocationCordinate(0, 10, 0);
                } 
                else
                {
                    //Return back to hub
                    fade.LocationCordinate(0, 10, 0);
                }
                Debug.Log("Entering Portal");
                fade.FadeOut();
            }
        }
        else if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interectionDistance, layerMaskTop))
        {
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interectionDistance, Color.green);
            gameManagerScript.lookingAtItem = true;

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("Combining");
                gameManagerScript.questTopDone = true;
            }
        }
        else if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interectionDistance, layerMaskRitual))
        {
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interectionDistance, Color.green);
            gameManagerScript.lookingAtItem = true;

            if (Input.GetKeyDown("e"))
            {
                fade.FadeOut();
            }
        }
        else
        {
            gameManagerScript.lookingAtItem = false;
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interectionDistance, Color.red);
        }
    }

    // Checks and handles movement of the player
    // Returns True if user input is detected and player is moved.
    private bool CheckMovement()
    {
        Vector3 newPos = transform.position;

        Vector3 xMovement = transform.right * Input.GetAxis("Horizontal");
        Vector3 zMovement = transform.forward * Input.GetAxis("Vertical");

        Vector3 movementVector = xMovement + zMovement;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed * 1.5f;
            Debug.Log("Sprinting");
        }
        else
        {
            moveSpeed = walkSpeed;
            Debug.Log("Walking");
        }

        if (movementVector.sqrMagnitude > 0)
        {
            movementVector *= moveSpeed * Time.deltaTime;
            newPos += movementVector;

            transform.position = newPos;
            return false;
        }
        else
        {
            return true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
        numOfJump = 1;
    }
    
    /// <summary>
    /// Shakes the screen based on the strength and duration
    /// </summary>
    /// <param name="duration">How long the shake last</param>
    /// <param name="strength">How powerful the shake is</param>
    /// <returns></returns>
    IEnumerator ScreenShake(float duration, float strength)
    {
        Vector3 originalPos = cameraParent.transform.localPosition;

        float timePass = 0.0f;
        while(timePass < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * strength;
            float y = Random.Range(-0.5f, 0.5f) * strength;

            playerCamera.transform.localPosition = new Vector3(x, y, originalPos.z);

            timePass += Time.deltaTime;

            yield return null;
        }

        cameraParent.transform.localPosition = new Vector3(originalPos.x, originalPos.y, originalPos.z);
    }
}
