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

    [SerializeField]
    private float interectionDistance;

    private int currentStage;

    /// The camera attached to the player model should be dragged in from Inspector.
    public Camera playerCamera;

    private string currentState;
    private string nextState;

    public Vector3 playerRotation;
    public Vector3 cameraRotation;

    public GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        nextState = "Idle";

        playerRotation = Vector3.zero;
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

        //Set the layermask to the quest
        //Only item with the quest will be affected 
        int layerMaskQuest = 1 << LayerMask.NameToLayer("Quest");
        int layerMaskPortal = 1 << LayerMask.NameToLayer("Portal");

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
                if (hitInfo.collider.gameObject.name == "Magic Stone")
                {
                    ++gameManagerScript.magicStone;
                    Debug.Log("Collecting magic stone");
                }
                else if (hitInfo.collider.gameObject.name == "Water")
                {
                    ++gameManagerScript.waterBottle;
                    Debug.Log("Collecting water");
                }
                else if (hitInfo.collider.gameObject.name == "Flower")
                {
                    ++gameManagerScript.flower;
                    Debug.Log("Collecting flower");
                }
            }
        }
        else if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interectionDistance, layerMaskPortal))
        {
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interectionDistance, Color.green);
            gameManagerScript.lookingAtItem = true;

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("Entering Portal");
            }
        }
        else
        {
            gameManagerScript.lookingAtItem = false;
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interectionDistance, Color.red);
        }


        //Teleports player back to the platform if they fall off the island
        if(this.transform.position.y < -10)
        {
            Debug.Log("Falling into the void");
            this.transform.position = new Vector3(4, 0, 0);
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
            moveSpeed = walkSpeed * 2.2f;
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
}
