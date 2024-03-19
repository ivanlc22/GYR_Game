using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool onPause = false;
    [SerializeField] private GameObject uiPause;

    void Start()
    {
        Cursor.visible = false; // Oculta el cursor al principio
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al principio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !onPause)
        {
            //Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None; // Libera el cursor para que puedas hacer clic
            gameObject.GetComponent<SC_FPSController>().enabled = false;
            uiPause.SetActive(true);
            onPause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && onPause)
        {
            //Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor nuevamente
            gameObject.GetComponent<SC_FPSController>().enabled = true;
            uiPause.SetActive(false);
            onPause = false;
        }
    }
}
