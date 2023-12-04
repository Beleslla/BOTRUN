using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public float bulletSpeed = 4f;
    private Transform playerTransform;

    void Start()
    {
        // Encuentra la referencia al jugador (asumiendo que el jugador tiene la etiqueta "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        MoveTowardsPlayer();

        if (!IsVisible())
        {
            Destroy(gameObject);
        }
    }

    void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, bulletSpeed * Time.deltaTime);

            // Calcula el ángulo de rotación basado en la dirección hacia el jugador
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }

    bool IsVisible()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        return (viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1);
    }
}
