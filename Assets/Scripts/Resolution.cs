using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    public bool fullscreen = true; 
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(640, 480, fullscreen);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
