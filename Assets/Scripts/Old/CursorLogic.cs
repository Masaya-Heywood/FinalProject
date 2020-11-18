using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CursorLogic : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 selectSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private bool setCursor = false;

    // Changes the cursor directly using a dedicated cursor asset.
    void Start()
    {
        //cursorTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Cursors/cursor1White.png", typeof(Texture2D));
        selectSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2); //calculates the middle of the texture for selection
        Cursor.SetCursor(cursorTexture, selectSpot, cursorMode);
    }

    private void Update()
    {
        if (!setCursor)
        {
            //set the cursor in the first call of Update
            selectSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2); //calculates the middle of the texture for selection
            Cursor.SetCursor(cursorTexture, selectSpot, cursorMode);
            setCursor = true;
        }
    }
}
