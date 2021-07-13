using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    private int currentQuestStage;
    private bool questGiven;

    public int[] stageOneQuest;

    public TextMeshProUGUI dialogueText;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentQuestStage = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckStage()
    {
        if(questGiven == false)
        {
            Debug.Log("Giving Quest");
            if (currentQuestStage == 0)
            {
                stageOneQuest = new int[3] { 1, 3, 3 };
                GiveStageOneQuest();
                gameManager.currentStage = currentQuestStage;
            } else if (currentQuestStage == 1)
            {
                stageOneQuest = new int[3] { 1, 3, 0};
                GiveStageTwoQuest();
                gameManager.questTopDone = false;
            }
        }
        if (currentQuestStage == 0 && gameManager.CheckCompletedQuest())
        {
            currentQuestStage += 1;
            gameManager.currentStage = currentQuestStage;
            Debug.Log("Gonna give you part two quest");
            questGiven = false;
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
        gameManager.SetQuestTwoQuest(stageOneQuest[0], stageOneQuest[1]);
    }
}
