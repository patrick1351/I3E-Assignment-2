/******************************************************************************
Author: Elyas Chua-Aziz

Name of Class: DemoPlayer

Description of Class: This class will control the movement and actions of a 
                        player avatar based on user input.

Date Created: 09/06/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayer : MonoBehaviour
{
    /// <summary>
    /// The distance this player will travel per second.
    /// </summary>
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float interectionDistance;

    /// <summary>
    /// The camera attached to the player model.
    /// Should be dragged in from Inspector.
    /// </summary>
    public Camera playerCamera;

    private string currentState;

    private string nextState;

    public Vector3 playerRotation;
    public Vector3 cameraRotation;

    public GameManager gameManagerScript;

    void Awake()
    {
        playerRotation = transform.rotation.eulerAngles;
        cameraRotation = playerCamera.transform.rotation.eulerAngles;

    }

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
        int layerMask = 1 << LayerMask.NameToLayer("Quest");

        //Draw line will show green if hit and red if no hit
        RaycastHit hitInfo;
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interectionDistance, layerMask))
        {
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interectionDistance, Color.green);
            gameManagerScript.lookAtItem = true;
            if (Input.GetKeyDown("e") && hitInfo.collider.gameObject.name == "Magic Stone")
            {
                ++gameManagerScript.magicStone;
                Debug.Log("Collecting magic stone");
            }
            
            
        } else
        {
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interectionDistance, Color.red);
            gameManagerScript.lookAtItem = false;
        }
    }

    /// <summary>
    /// Sets the current state of the player
    /// and starts the correct coroutine.
    /// </summary>
    private void SwitchState()
    {
        StopCoroutine(currentState);

        currentState = nextState;
        StartCoroutine(currentState);
    }

    private IEnumerator Idle()
    {
        while(currentState == "Idle")
        {
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
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
    /// Checks and handles movement of the player
    /// </summary>
    /// <returns>True if user input is detected and player is moved.</returns>
    private bool CheckMovement()
    {
        Vector3 newPos = transform.position;

        Vector3 xMovement = transform.right * Input.GetAxis("Horizontal");
        Vector3 zMovement = transform.forward * Input.GetAxis("Vertical");

        Vector3 movementVector = xMovement + zMovement;

        if(movementVector.sqrMagnitude > 0)
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
