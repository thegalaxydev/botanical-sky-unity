using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorDefaultTexture;
    [SerializeField]
    private Texture2D _cursorMouseDownTexture;
    [SerializeField]
    private Vector2 _cursorHotspot = Vector2.zero;


    private void Start() {
        Cursor.SetCursor(_cursorDefaultTexture, _cursorHotspot, CursorMode.Auto);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(_cursorMouseDownTexture, _cursorHotspot, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(_cursorDefaultTexture, _cursorHotspot, CursorMode.Auto);
        }   
    }
}
