using UnityEngine;
using System.Collections;

public class MovimientoAleatorio : MonoBehaviour
{
    public float velocidad = 2.75f;
    public float tiempoCambioDireccion = 1.25f;
    public float margenBordes = 1f; // Ajusta estos valores según sea necesario
    private float tiempoParaCambio;
    private Vector2 direccionActual;

    private bool puedeMoverse = true;

    private Animator anim;
    private Rigidbody2D body;

    private float velocidadTemporal;

    private void Start()
    {
        direccionActual = NuevaDireccion();
        tiempoParaCambio = tiempoCambioDireccion;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        velocidadTemporal = velocidad;
    }

    private void Update()
    {
        if (puedeMoverse)
        {
            transform.Translate(direccionActual * velocidad * Time.deltaTime);
            CrearBordes();
            tiempoParaCambio -= Time.deltaTime;

            if (tiempoParaCambio <= 0f)
            {
                direccionActual = NuevaDireccion();
                tiempoParaCambio = tiempoCambioDireccion;
                StartCoroutine(AumentarVelocidadTemporalmente(0.2f));

                ActualizarAnimacion();
            }
        }
    }

    private void ActualizarAnimacion()
    {
        float angle = Mathf.Atan2(direccionActual.y, direccionActual.x) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }

        string direccion = "Idle"; // Valor predeterminado

        if (angle >= 45 && angle < 135)
        {
            direccion = "Up";
        }
        else if (angle >= 135 && angle < 225)
        {
            direccion = "Left";
        }
        else if (angle >= 225 && angle < 315)
        {
            direccion = "Down";
        }
        else if (angle >= 315 || angle < 45)
        {
            direccion = "Right";
        }

        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);

        switch (direccion)
        {
            case "Up":
                anim.SetBool("Up", true);
                break;
            case "Down":
                anim.SetBool("Down", true);
                break;
            case "Left":
                anim.SetBool("Left", true);
                break;
            case "Right":
                anim.SetBool("Right", true);
                break;
            default:
                // Si es Idle, no activamos ningún bool adicional
                break;
        }

        anim.SetTrigger(direccion);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(DesactivarMovimientoTemporalmente(1.5f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            direccionActual = Vector2.Reflect(direccionActual, collision.contacts[0].normal).normalized;

            StartCoroutine(AumentarVelocidadTemporalmente(0.3f));
        }
    }

    private IEnumerator DesactivarMovimientoTemporalmente(float duracion)
    {
        puedeMoverse = false;
        ActualizarAnimacion();
        yield return new WaitForSeconds(duracion);
        puedeMoverse = true;
    }

    private IEnumerator AumentarVelocidadTemporalmente(float duracion)
    {

        velocidadTemporal *= 1.5f;
        yield return new WaitForSeconds(duracion);
        velocidadTemporal = velocidad;
    }

    private void CrearBordes()
    {
        float anchoPantalla = Camera.main.orthographicSize * Screen.width / Screen.height;
        float altoPantalla = Camera.main.orthographicSize;
        Vector3 posicionActual = transform.position;

        posicionActual.x = Mathf.Clamp(posicionActual.x, -anchoPantalla + margenBordes, anchoPantalla - margenBordes);
        posicionActual.y = Mathf.Clamp(posicionActual.y, -altoPantalla + margenBordes, altoPantalla - margenBordes);

        transform.position = posicionActual;
    }
    private Vector2 NuevaDireccion()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }


}
