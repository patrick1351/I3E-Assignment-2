/******************************************************************************
Author: Patrick
Name of Class: I3E
Description of Class: 
Date Created: 5/6/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    private int currentQuestStage;
    private bool questGiven;

    public int[] stageOneQuest;
    public int[] stageTwoQuest;
    public int[] stageThreeQuest;

    public TextMeshProUGUI dialogueText;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentQuestStage = 0;
    }

    public void CheckStage()
    {
        if(questGiven == false)
        {
            Debug.Log("Giving Quest");
            if (currentQuestStage == 0)
            {
                stageOneQuest = new int[3] { 1, 1, 1 };
                GiveStageOneQuest();
                gameManager.currentStage = currentQuestStage;
            } else if (currentQuestStage == 1)
            {
                stageTwoQuest = new int[3] { 2, 1, 0};
                GiveStageTwoQuest();
                gameManager.questTopDone = false;
            }
            else if (currentQuestStage == 2)
            {
                stageThreeQuest = new int[3] { 5, 2, 1 };
                GiveStageThreeQuest();
            }
        }
        if (currentQuestStage == 0 && gameManager.CheckCompletedQuest())
        {
            gameManager.transitioning = true;
            currentQuestStage += 1;
            gameManager.currentStage = currentQuestStage;
            Debug.Log("Gonna give you part two quest");
            questGiven = false;
        } else if (currentQuestStage == 1 && gameManager.CheckCompletedQuest())
        {
            gameManager.transitioning = true;
            currentQuestStage += 1;
            gameManager.currentStage = currentQuestStage;
            Debug.Log("Gonna give you part three quest");
            questGiven = false;
        } else
        {
            Debug.Log("You have not completed all your quest");
        }
    }

    void GiveStageOneQuest()
    {
        if(questGiven == false)
        {
            gameManager.ResetItemCount();
            questGiven = true;
        }
        gameManager.SetQuestOneQuest(stageOneQuest[0], stageOneQuest[1], stageOneQuest[2]);
    }

    void GiveStageTwoQuest()
    {
        if (questGiven == false)
        {
            gameManager.ResetItemCount();
            questGiven = true;
        }
        gameManager.SetQuestTwoQuest(stageTwoQuest[0], stageTwoQuest[1]);
    }

    void GiveStageThreeQuest()
    {
        if (questGiven == false)
        {
            gameManager.ResetItemCount();
            questGiven = true;
        }
        gameManager.SetQuestThreeQuest(stageThreeQuest[0], stageThreeQuest[1], stageThreeQuest[2]);
    }
}
