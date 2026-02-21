using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public Slider healthSlider;

    [Header("Health Bar Images (Optional)")]
    public Image[] healthBarImages;

    public GameObject explosionVFX;

    public Animator animator;
    public Rigidbody2D rb;
    public float knockbackForce = 5f;

    public event Action OnDamaged;

    void Start()
    {
        currentHealth = maxHealth;
        
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
        
        UpdateHealthBarImages();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);

            Vector2 hitDir = transform.position.x < collision.transform.position.x ? Vector2.left : Vector2.right;
            
            if (rb != null)
            {
                rb.AddForce(new Vector2(hitDir.x * knockbackForce, 0), ForceMode2D.Impulse);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        
        OnDamaged?.Invoke();
        
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        
        UpdateHealthBarImages();

        if (currentHealth <= 0)
        {
            if (explosionVFX != null)
            {
                Instantiate(explosionVFX, gameObject.transform.position, Quaternion.identity);
            }
            Debug.Log("Player Died!");
            Destroy(gameObject);
        }
    }

    void UpdateHealthBarImages()
    {
        if (healthBarImages == null || healthBarImages.Length == 0) return;

        for (int i = 0; i < healthBarImages.Length; i++)
        {
            if (healthBarImages[i] != null)
            {
                healthBarImages[i].enabled = i < currentHealth;
            }
        }
    }
}
