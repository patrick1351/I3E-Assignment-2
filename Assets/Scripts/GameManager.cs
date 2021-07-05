using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Texture2D crosshairRed;
    public Texture2D crosshairGreen;
    private Texture2D crosshair;
    public bool lookingAtItem;

    public int magicStone;

    public TextMeshProUGUI questUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lookingAtItem)
        {
            crosshair = crosshairGreen;
        }
        else
        {
            crosshair = crosshairRed;
        }

        Vector2 cursorLocation = new Vector2(crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorLocation, CursorMode.Auto);

        questUI.SetText("Quest\n" +
                        "Magic Stone: {0}", magicStone);
    }
}
