using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    private int currentQuestStage;
    private bool questGiven;

    public int[] stageOneQuest = new int[3] { 1, 3, 3 };

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
        if(currentQuestStage == 0)
        {
            GiveStageOneQuest();
            gameManager.currentStage = currentQuestStage;
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
}
