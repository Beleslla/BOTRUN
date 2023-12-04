using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambiadorDeImagen : MonoBehaviour
{
    public Sprite[] imagenes; // Asigna tus sprites en el Inspector
    private int indiceActual = 0;
    private Image imagen;

    void Start()
    {
        imagen = GetComponentInChildren<Image>();

        // Asegúrate de tener al menos una imagen asignada
        if (imagen != null && imagenes != null && imagenes.Length > 0)
        {
            imagen.sprite = imagenes[indiceActual];
        }
        else
        {
            Debug.LogError("Asigna al menos una imagen y un objeto Image en el Inspector.");
        }
    }

    void Update()
    {
        // Cambia la imagen al hacer clic con el ratón
        if (Input.GetMouseButtonDown(0))
        {
            CambiarImagen();
        }
    }

    void CambiarImagen()
    {
        // Asegúrate de tener al menos una imagen asignada
        if (imagenes != null && imagenes.Length > 0)
        {
            // Incrementa el índice y muestra la siguiente imagen
            indiceActual++;

            if (indiceActual < imagenes.Length)
            {
                imagen.sprite = imagenes[indiceActual];
            }
            else
            {
                // Si hemos mostrado todas las imágenes, cambia a la escena del menú
                SceneManager.LoadScene("Menu");
            }
        }
    }
}