using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public GameObject chispazo;
    private BoxCollider2D col;
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shield")
        {
            Vector3 posicionActual = transform.position;
            GameObject.Destroy(this.gameObject);
            chispazo = Instantiate(chispazo, posicionActual, Quaternion.identity);
            
            var sistemaParticulas = chispazo.GetComponent<ParticleSystem>();
            if (sistemaParticulas != null)
            {
                sistemaParticulas.Clear();
                sistemaParticulas.Play();
            }
        }
        else if (collision.tag == "Player")
        {
            Vector3 posicionActual = transform.position;
            GameObject.Destroy(this.gameObject);
            chispazo = Instantiate(chispazo, posicionActual, Quaternion.identity);

            var sistemaParticulas = chispazo.GetComponent<ParticleSystem>();
            if (sistemaParticulas != null)
            {
                sistemaParticulas.Clear();
                sistemaParticulas.Play();
            }
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }
        }
    }
}