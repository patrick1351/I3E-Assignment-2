using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    private int currentQuestStage;

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
        }
    }

    void GiveStageOneQuest()
    {
        gameManager.toCollectMagicStone = 1;
        gameManager.toCollectFlower = 4;
        gameManager.toCollectWaterBottle = 5;
        gameManager.stageOneQuest = new int[] { 1, 4, 5 };
    }
}
