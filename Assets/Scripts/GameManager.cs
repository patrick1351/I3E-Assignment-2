using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject crosshairRed;
    public GameObject crosshairGreen;
    public bool lookingAtItem;

    public int magicStone;
    public int flower;
    public int waterBottle;

    public TextMeshProUGUI questUI;
    
    // Start is called before the first frame update
    void Start()
    {
        crosshairRed.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        crosshairGreen.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        

        //Change color of crosshair if the player is looking at interactble item
        if (lookingAtItem)
        {
            crosshairRed.SetActive(false);
            crosshairGreen.SetActive(true);
        } else
        {
            crosshairRed.SetActive(true);
            crosshairGreen.SetActive(false);
        }

        questUI.SetText("Quest\n" +
                        "Magic Stone: {0}\n" +
                        "Flower: {1}", magicStone, flower);
    }
}
