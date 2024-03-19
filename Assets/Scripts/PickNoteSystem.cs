using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickNoteSystem : MonoBehaviour
{
    public float raycastDistance = 10f; // La distancia máxima del raycast.
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject uipage;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip pageclip;
    [SerializeField] private AudioClip closedDoorClip;
    [SerializeField] private AudioClip pickupKeyClip;
    [SerializeField] private Texture2D[] pages;
    [SerializeField] private int currentPage = 0;
    [SerializeField] private Text text;
    [SerializeField] private GameObject text2;

    private string pageText = "Read Page (E)";
    private string keyText = "Pick up the Key (E)";
    private string doorText = "Enter home (E)";

    [SerializeField] private bool hasKey;
    [SerializeField] private bool openFence;

    [SerializeField] private GameObject fenceOpenModel;
    [SerializeField] private GameObject fenceCloseModel;
    [SerializeField] private GameObject[] trafficLights;

    private float currentTime = 0;
    private float textDuration = 2f;

    public Checkpoint checkpoint;

    private void Start()
    {
        for(int i=0; i<trafficLights.Length; i++) 
        {
            trafficLights[i].SetActive(false);
        }
    }


    void Update()
    {
        // Obtén la posición y dirección de la cámara.
        Vector3 raycastOrigin = cam.transform.position;
        Vector3 raycastDirection = cam.transform.forward;

        // Realiza el raycast.
        RaycastHit hit;

        if (uipage.activeSelf && !ui.activeSelf) 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                Time.timeScale = 1;
                uipage.SetActive(false);
                currentPage++;
                gameObject.GetComponent<SC_FPSController>().enabled = true;
                gameObject.transform.GetChild(2).GetComponent<AudioSource>().mute = false;
            }
        }

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance))
        {
            if(hit.collider.tag == "Note" && !uipage.activeSelf) 
            {
                text.text = pageText;
                ui.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.GetComponent<SC_FPSController>().enabled = false;
                    gameObject.transform.GetChild(2).GetComponent<AudioSource>().mute = true;

                    audiosource.clip = pageclip;
                    audiosource.Play();

                    ui.SetActive(false);
                    uipage.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(pages[currentPage], new Rect(0, 0, pages[0].width, pages[0].height), new Vector2(0.5f, 0.5f));
                    uipage.SetActive(true);
                    Destroy(hit.collider.gameObject);
                    Time.timeScale = 0;

                    checkpoint.updatePosition();
                }
            }
           else if(hit.collider.tag == "Key") 
           {
                text.text = keyText;
                ui.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    hasKey = true;
                    audiosource.clip = pickupKeyClip;
                    audiosource.Play();
                    Destroy(hit.collider.gameObject);
                    ui.SetActive(false);

                    for(int i=0; i<trafficLights.Length; i++)
                    {
                        trafficLights[i].SetActive(true);
                    }
                }
                    
           }
           else if (hit.collider.tag == "Door" && !audiosource.isPlaying)
           {
                text.text = doorText;
                ui.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E) && !hasKey)
                {
                    if (!fenceOpenModel.activeSelf) 
                    {
                        fenceOpenModel.SetActive(true);
                        fenceCloseModel.SetActive(false);
                    }

                    StartCoroutine(ClosedDoor());
                }
                else if(Input.GetKeyDown(KeyCode.E) && hasKey) 
                {
                    SceneManager.LoadScene("EndGame");
                }

           }
           else ui.SetActive(false);

        }
        else ui.SetActive(false);
    }


    private IEnumerator ClosedDoor() 
    {
        text.enabled = false;
        text2.gameObject.SetActive(true);
        audiosource.PlayOneShot(closedDoorClip);
        currentTime = 0;

        while (currentTime < textDuration) 
        {            
            currentTime += Time.deltaTime;
            yield return null;
        }

        text2.gameObject.SetActive(false);
        text.enabled = true;
        yield return null;

    }
}
