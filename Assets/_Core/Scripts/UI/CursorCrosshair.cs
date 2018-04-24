using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic
// It changes the Cursor into a Crosshair, given the right texture
public class CursorCrosshair : MonoBehaviour
{

	[SerializeField] Texture2D cursorTexture;
    [SerializeField] CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] Vector2 hotSpot;

    void Start ()
    {
        hotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

}
