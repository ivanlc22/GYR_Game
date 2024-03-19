using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    private enum Direction { Horizontal, Vertical };

    [SerializeField] private GameObject destination;
    [SerializeField] private Image blackImage;
    [SerializeField] private float fadeDuration = 2.0f;
    [SerializeField] private Direction type;

    private bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && other.CompareTag("Player"))
        {
            StartCoroutine(TeleportWithFade(other.gameObject));
        }
    }

    private IEnumerator TeleportWithFade(GameObject player)
    {
        print("Estoy teletransportandome...");
        isTeleporting = true;

        // Comienza el fundido a negro
        float elapsedTime = 0;
        Color initialColor = blackImage.color;
        Color targetColor = new Color(0, 0, 0, 1);
        blackImage.enabled = true;

        while (elapsedTime < fadeDuration)
        {
            blackImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackImage.color = targetColor;

        // Guarda la posición actual del jugador
        Vector3 playerPosition = player.transform.position;

        // Teletransporta al jugador
        if (type == Direction.Horizontal)
        {
            playerPosition.x = destination.transform.position.x;
        }
        else
        {
            playerPosition.z = destination.transform.position.z;
        }
       
        player.transform.GetComponent<SC_FPSController>().enabled = false;
        player.transform.position = playerPosition;
        player.transform.GetComponent<SC_FPSController>().enabled = true;

        // Inicia el fundido de regreso
        elapsedTime = 0;
        initialColor = targetColor;
        targetColor = new Color(0, 0, 0, 0);

        while (elapsedTime < fadeDuration)
        {
            blackImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackImage.color = targetColor;
        isTeleporting = false;
    }
}

