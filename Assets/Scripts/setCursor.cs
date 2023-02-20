using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCursor : MonoBehaviour
{
    public Texture2D cursorImg;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorOffset = new Vector2(cursorImg.width / 2, cursorImg.height / 2);
        Cursor.SetCursor(cursorImg, cursorOffset, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
