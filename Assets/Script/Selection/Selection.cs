using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public Texture2D selectionHighlight = null;
    public static Rect selection = new Rect(0, 0, 0, 0);
    private Vector3 startClick = -Vector3.one;

    private static Vector3 moveToDestination = new Vector3();
    private static List<string> passable;

    private void Start()
    {
        passable = new List<string>() { "Deco" };
    }
    // Update is called once per frame
    void Update()
    {
        CheckCamera();
        Cleanup();
    }

    private void CheckCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startClick = Input.mousePosition;
        } else if(Input.GetMouseButtonUp(0))
        {
            startClick = -Vector3.one;
        }
        if (Input.GetMouseButton(0))
        {
            selection = new Rect(startClick.x, InvertMouseY(startClick.y), Input.mousePosition.x - startClick.x, InvertMouseY(Input.mousePosition.y) - InvertMouseY(startClick.y));
            if (selection.width < 0)
            {
                selection.x += selection.width;
                selection.width = -selection.width;
            }
            if (selection.height < 0)
            {
                selection.y += selection.height;
                selection.height = -selection.height;
            }
        }
    }

    private void OnGUI()
    {
        if (startClick != -Vector3.one) {
            GUI.color = new Color(1, 1, 1, 0.5f);
            GUI.DrawTexture(selection, selectionHighlight);
        }
    } 
    public static float InvertMouseY(float y)
    {
        return Screen.height - y;
    }

    private void Cleanup()
    {
        if(!Input.GetMouseButtonUp(1))
        {
            moveToDestination = Vector3.zero;
        }
    }

    public static Vector3 GetDestination()
    {
        if(moveToDestination == Vector3.zero)
        {
            moveToDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        return moveToDestination;
    }
}
