using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class KillAnimManager : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject canvas;
    public GameObject camera;
    public Checkpoint checkpoint;

    public GameObject player;
    public GameObject monster;
    public GameObject monsterInitialPos;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Play();
        canvas.SetActive(true);
    }

    public void PlayerDeath()
    {
        // Obtiene el índice de la escena actual
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        // Carga la escena actual
        SceneManager.LoadScene(escenaActual);
    }
}
