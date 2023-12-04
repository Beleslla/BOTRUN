using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool invulnerable;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [SerializeField] private UIManager uiManager; //Temp

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage()
    {
        if (invulnerable) return;

        currentHealth = currentHealth - 1;

        if (currentHealth != 0)
        Invulnerability();
        else if (currentHealth == 0)
        {
            uiManager.GameOver(); //Temp
            Deactivate();
        }      
    }

    public IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            TakeDamage();
        }
    }
}
