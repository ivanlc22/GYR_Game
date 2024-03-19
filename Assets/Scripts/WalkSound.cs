using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioClip concrete;
    [SerializeField] private AudioClip grass;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float raycastDistance = 0.2f; 
    private string material = "Grass";
    private string previousHitTag = "";

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            // Aquí puedes acceder al material del suelo
            // Material groundMaterial = hit.collider.GetComponent<Renderer>().material;
            //

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (hit.collider.tag == "Grass")
                {
                    if (previousHitTag != "Grass")
                    {
                        // Cambio detectado de otro tag a "Grass"
                        // Realiza las acciones necesarias
                        sound.enabled = false;
                        previousHitTag = "Grass"; // Actualiza el tag anterior
                    }
                    sound.clip = grass;
                    sound.enabled = true;
                }
                else
                {
                    if (previousHitTag != "Concrete")
                    {
                        // Cambio detectado de otro tag a "Concrete"
                        // Realiza las acciones necesarias
                        sound.enabled = false;
                        previousHitTag = "Concrete"; // Actualiza el tag anterior
                    }
                    sound.clip = concrete;
                    sound.enabled = true;
                }
            }
            else
            {
                sound.enabled = false;
                previousHitTag = ""; // Resetea el tag anterior
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Cambia el color del raycast
        Vector3 raycastStart = transform.position;
        Vector3 raycastDirection = Vector3.down;
        Gizmos.DrawRay(raycastStart, raycastDirection * raycastDistance);
    }

    /*
    private void Update()
    {
        print(material);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (material == "Concrete" || material == "House" || material == "Sidewalk" || material == "Road")
            {
                sound.clip = concrete;
                sound.enabled = true;
            }

            if(material == "Grass")
            {
                sound.clip = grass;
                sound.enabled = true;
            }
        }
        else
        {
            sound.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        sound.enabled = false;
        material = other.GetComponent<Renderer>().sharedMaterial.name;
    }

    private void OnTriggerExit(Collider other)
    {
        sound.enabled = false;
        // material = "Grass";
    }
    */
}


