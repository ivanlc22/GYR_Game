using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject greenLight;


    private void Start()
    {
        ApagarLucesSemaforo();
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit() 
    {
        Application.Quit();
    }

    public void PantallaCompleta(bool full) 
    {
        Screen.fullScreen = full;
    }

    public void ChangeVolume(float volume) 
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume)*20);
    }

    public void ChangeToOption() 
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ExitOptions() 
    {
        yellowLight.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EncenderLuzRoja()
    {
        redLight.SetActive(true);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);
    }

    public void EncenderLuzAmarilla()
    {
        redLight.SetActive(false);
        yellowLight.SetActive(true);
        greenLight.SetActive(false);
    }

    public void EncenderLuzVerde()
    {
        redLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(true);
    }

    public void ApagarLucesSemaforo()
    {
        redLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);
    }


}
