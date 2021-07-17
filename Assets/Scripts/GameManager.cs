/******************************************************************************
Author: Patrick
Name of Class: I3E
Description of Class: 
Date Created: 28/6/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //These two are the crosshair image
    public GameObject crosshairRed;
    public GameObject crosshairGreen;

    public GameObject ritual;
    public GameObject heartOne;
    public GameObject heartTwo;

    /// <summary>
    /// The bool to check if player currently looking at item
    /// </summary>
    public bool lookingAtItem;

    public int currentStage;
    public bool transitioning;

    public int magicStone;
    public int flower;
    public int waterBottle;
    public int pillarActivated;

    public int toCollectMagicStone;
    public int toCollectFlower;
    public int toCollectWaterBottle;
    public int toActivatePillar;

    /// <summary>
    /// The quest that requires you to collect item
    /// </summary>
    public bool questTopDone;
    public bool allPillarActivated;

    /// <summary>
    /// The array that ensures all item are collected
    /// </summary>
    public bool[] itemCollected = new bool[] {false, false, false};

    public TextMeshProUGUI questUI;
    public Fade fade;
    
    // Start is called before the first frame update
    void Start()
    {
        toActivatePillar = 4;
        currentStage = -1;
        crosshairRed.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        crosshairGreen.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        SetCrosshair();
        CheckCollectedItem();
        CheckCompletedQuest();

        SetText();
        if (allPillarActivated)
        {
            ritual.SetActive(true);
        }
    }

    void SetCrosshair()
    {
        //Change color of crosshair if the player is looking at interactble item
        if (lookingAtItem)
        {
            crosshairRed.SetActive(false);
            crosshairGreen.SetActive(true);
        }
        else
        {
            crosshairRed.SetActive(true);
            crosshairGreen.SetActive(false);
        }
    }

    public void SetQuestOneQuest(int magicStone, int flower, int waterBottle)
    {
        transitioning = false;
        toCollectFlower = flower;
        toCollectMagicStone = magicStone;
        toCollectWaterBottle = waterBottle;
    }
    public void SetQuestTwoQuest(int magicStone, int flower)
    {
        transitioning = false;
        toCollectFlower = flower;
        toCollectMagicStone = magicStone;
    }
    public void SetQuestThreeQuest(int magicStone, int flower, int waterBottle)
    {
        transitioning = false;
        toCollectFlower = flower;
        toCollectMagicStone = magicStone;
        toCollectWaterBottle = waterBottle;
    }

    void CheckCollectedItem()
    {
        if(magicStone >= toCollectMagicStone)
        {
            Debug.Log("Collected all Magic Stone");
            magicStone = toCollectMagicStone;
            itemCollected[0] = true;
        }

        if (flower >= toCollectFlower)
        {
            Debug.Log("Collected all Flower");
            flower = toCollectFlower;
            itemCollected[1] = true;
        }

        if (waterBottle >= toCollectWaterBottle)
        {
            Debug.Log("Collected all Water Bottle");
            waterBottle = toCollectWaterBottle;
            itemCollected[2] = true;
        }
        
        if(pillarActivated == toActivatePillar)
        {
            allPillarActivated = true;
        }
    }

    public bool CheckCompletedQuest()
    {
        if(currentStage == 0)
        {
            if (itemCollected[0] && itemCollected[1] && itemCollected[2])
            {
                Debug.Log("Well done, stage one completed");
                return true;
            }
        } 
        else if (currentStage == 1)
        {
            if(itemCollected[0] && itemCollected[1])
            {
                Debug.Log("Well done, stage two completed");
                return true;
            }
        }
        else if (currentStage == 2)
        {
            if (itemCollected[0] && itemCollected[1] && itemCollected[2])
            {
                Debug.Log("Well done, stage Three completed");
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Changes the text accordingly when the quest is done 
    /// </summary>
    void SetText()
    {
        if (transitioning)
        {
            questUI.SetText("Click on me when you are ready");
        }
        else if (currentStage == 0)
        {
            if (questTopDone)
            {
                questUI.SetText("Go back to the quest giver");
            } 
            else if (CheckCompletedQuest())
            {
                heartOne.SetActive(true);
                questUI.SetText("Go and collect the heart of the island at the enterence");
            } 
            else
            {
                questUI.SetText("Quest\n" +
                        "Magic Stone: {0}/{1} \n" +
                        "Flower: {2}/{3} \n" +
                        "WaterBottle: {4}/{5}", magicStone, toCollectMagicStone, flower, toCollectFlower, waterBottle, toCollectWaterBottle);
            }
            
        } 
        else if (currentStage == 1)
        {
            if (questTopDone)
            {
                questUI.SetText("Go back to the quest giver");
            }
            else if (CheckCompletedQuest())
            {
                heartTwo.SetActive(true);
                questUI.SetText("Go and collect the heart of the island at the enterence");
            }
            else
            {
                questUI.SetText("Quest\n" +
                        "Magic Stone: {0}/{1} \n" +
                        "Flower: {2}/{3}", magicStone, toCollectMagicStone, flower, toCollectFlower);
            }
        }
        else if (currentStage == 2)
        {
            if (allPillarActivated)
            {
                questUI.SetText("Go and activate the ritual");
            }
            else if (CheckCompletedQuest())
            {
                questUI.SetText("Activate all the pillars\n" +
                        "Pillars activated: {0}/{1}", pillarActivated, toActivatePillar);
            }
            else
            {
                questUI.SetText("Quest\n" +
                        "Magic Stone: {0}/{1} \n" +
                        "Water Bottle: {2}/{3} \n" +
                        "WaterBottle: {4}/{5}", magicStone, toCollectMagicStone, flower, toCollectFlower, waterBottle, toCollectWaterBottle);
            }
        }
    }

    /// <summary>
    /// Resets the quest tracked counter
    /// </summary>
    public void ResetItemCount()
    {
        magicStone = 0;
        flower = 0;
        waterBottle = 0;

        itemCollected = new bool[] { false, false, false };
    }
}
