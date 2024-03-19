using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryEvent : MonoBehaviour
{
    [SerializeField] private Image blackoutImage; // La imagen que representa la pantalla en negro.
    [SerializeField] private float fadeInDuration = 2.0f; // Duración del efecto de desvanecimiento.
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip closeDoor;
    [SerializeField] private AudioSource audioSource;

    private float currentTime = 0;

    private void Start()
    {
        gameObject.transform.GetChild(2).GetComponent<AudioSource>().mute = true;
        StartCoroutine(PlayOpenDoorSoundAndFade());
    }

    private IEnumerator PlayOpenDoorSoundAndFade()
    {
        // Reproducir el sonido openDoor
        audioSource.PlayOneShot(openDoor);

        // Pausar la ejecución durante la duración del sonido openDoor
        yield return new WaitForSeconds(openDoor.length);

        audioSource.PlayOneShot(closeDoor);

        yield return new WaitForSeconds(closeDoor.length+2);

        // Continuar con el desvanecimiento
        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = 1 - (currentTime / fadeInDuration);
            blackoutImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        blackoutImage.enabled = false;
        gameObject.GetComponent<CharacterController>().enabled = true;
        gameObject.transform.GetChild(2).GetComponent<AudioSource>().mute = false;

        // Ocultar la imagen cuando el desvanecimiento haya terminado.
        //blackoutImage.gameObject.SetActive(false);
    }
}
