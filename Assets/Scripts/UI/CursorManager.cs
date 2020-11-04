using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
    void Start()
    {
        //set cursor
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

}
