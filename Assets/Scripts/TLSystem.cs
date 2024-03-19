using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TLSystem : MonoBehaviour
{
    private enum type { permanent, changing };
    [SerializeField] private type tlType;

    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject greenLight;
    [SerializeField] private GameObject tp;
    [SerializeField] private GameObject monster;


    private float currentTime = 0;
    [SerializeField] private float durationLight = 2;
    private int beforeLight;
    private int lightState = 0;

    private bool detected = false;

    public AudioSource audioSource;
    public AudioClip switchSound;

    // Start is called before the first frame update
    void Start()
    {
        if(tlType == type.permanent) 
        {
            redLight.GetComponent<Light>().enabled = true;
            yellowLight.GetComponent<Light>().enabled = false;
            greenLight.GetComponent<Light>().enabled = false;
        }
        else 
        {
            redLight.GetComponent<Light>().enabled = false;
            yellowLight.GetComponent<Light>().enabled = false;
            greenLight.GetComponent<Light>().enabled = false;
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tlType == type.changing) 
        {
            currentTime += Time.deltaTime;

            if (currentTime >= durationLight) 
            {
                switchLight();
                currentTime = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && redLight.GetComponent<Light>().enabled && !detected)
        {
            detected = true;

            monster.GetComponent<NavMeshAgent>().enabled = false;
            monster.transform.position = new Vector3(tp.transform.position.x, monster.transform.position.y, tp.transform.position.z);
            monster.GetComponent<NavMeshAgent>().enabled = true;
        }
    }


    private void switchLight() 
    {
        lightState = Random.Range(0, 3);

        if (beforeLight == 0)
        {
            lightState = 2;
        }

        if (beforeLight == 2)
        {
            lightState = 1;
        }

        if (beforeLight == 1)
        {
            lightState = 0;
        }

        if (beforeLight != lightState)
        {
            audioSource.PlayOneShot(switchSound);
        }
        
        redLight.GetComponent<Light>().enabled = false;
        yellowLight.GetComponent<Light>().enabled = false;
        greenLight.GetComponent<Light>().enabled = false;

        switch (lightState) 
        {
            case 0:
                redLight.GetComponent<Light>().enabled = true;
                beforeLight = 0;
                break;
            case 1:
                yellowLight.GetComponent<Light>().enabled = true;
                beforeLight = 1;
                break;
            case 2:
                greenLight.GetComponent<Light>().enabled = true;
                beforeLight = 2;
                break;
        }
    }
}
