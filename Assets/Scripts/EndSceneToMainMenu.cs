using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    public string nombreDeEscena; // Nombre de la escena a la que deseas cambiar
    public float tiempoDeEspera = 10.0f; // Tiempo en segundos antes del cambio

    void Start()
    {
        // Llama al método CambiarEscena después de un tiempo de espera
        Invoke("CambiarEscena", tiempoDeEspera);
    }

    void CambiarEscena()
    {
        // Cambia a la escena deseada
        SceneManager.LoadScene(nombreDeEscena);
    }
}