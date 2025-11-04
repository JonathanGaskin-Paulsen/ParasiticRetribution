using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrossHairTracking : MonoBehaviour
{

    public Texture2D CursorTexture;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        Cursor.SetCursor(CursorTexture,Vector2.zero,CursorMode.Auto);
        //Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9);
        transform.position = Camera.main.ScreenToWorldPoint(cursorPos);
    }
}