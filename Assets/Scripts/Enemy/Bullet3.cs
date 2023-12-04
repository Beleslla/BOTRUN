using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    public float velocidad = 4f;
    public float tiempoCambioDireccion = 1.25f;
    public float margenBordes = 1f; // Ajusta estos valores según sea necesario

    private float tiempoParaCambio;
    private Vector2 direccionActual;
    private int toquesConShield = 0;
    public int toquesNecesarios = 3;

    private void Start()
    {
        // Inicializa la dirección inicial y el tiempo para el primer cambio de dirección
        direccionActual = NuevaDireccion();
        tiempoParaCambio = tiempoCambioDireccion;
    }

    private void Update()
    {
        // Mueve el jugador en la dirección actual
        transform.Translate(direccionActual * velocidad * Time.deltaTime);

        // Ajusta la posición para evitar que el jugador se salga de los bordes
        CrearBordes();

        // Reduce el tiempo restante para el próximo cambio de dirección
        tiempoParaCambio -= Time.deltaTime;

        // Si es tiempo de cambiar de dirección, genera una nueva dirección aleatoria
        if (tiempoParaCambio <= 0f)
        {
            direccionActual = NuevaDireccion();
            tiempoParaCambio = tiempoCambioDireccion;
        }
    }

    private Vector2 NuevaDireccion()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void CrearBordes()
    {
        // Obtiene las dimensiones de la pantalla en unidades del mundo
        float anchoPantalla = Camera.main.orthographicSize * Screen.width / Screen.height;
        float altoPantalla = Camera.main.orthographicSize;

        // Obtiene la posición actual del jugador
        Vector3 posicionActual = transform.position;

        // Limita la posición en los bordes de la pantalla
        posicionActual.x = Mathf.Clamp(posicionActual.x, -anchoPantalla + margenBordes, anchoPantalla - margenBordes);
        posicionActual.y = Mathf.Clamp(posicionActual.y, -altoPantalla + margenBordes, altoPantalla - margenBordes);

        // Actualiza la posición del jugador
        transform.position = posicionActual;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            // Incrementa el contador de toques
            toquesConShield++;

            // Si alcanza el número necesario de toques, destruye la bala
            if (toquesConShield >= toquesNecesarios)
            {
                Destroy(gameObject);
            }
        }
    }
}