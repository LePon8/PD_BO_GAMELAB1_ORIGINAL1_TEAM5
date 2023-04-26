using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D normalCursor;
    [SerializeField] Vector2 normalCursorHS = new(10,10);

    [SerializeField] Texture2D handCursor;
    [SerializeField] Vector2 handCursorHS = new(20,20);

    public void ToHandCursor()
    {
        Cursor.SetCursor(handCursor, handCursorHS, CursorMode.Auto);
    }

    public void ToNormalCursor()
    {
        Cursor.SetCursor(normalCursor, normalCursorHS, CursorMode.Auto);
    }

}
