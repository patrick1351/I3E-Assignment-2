using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Texture2D crosshair;

    [SerializeField]
    private TextMeshProUGUI questUI;

    public int magicStone;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorLocation = new Vector2(crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorLocation, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        questUI.text = magicStone.ToString();
    }


}
