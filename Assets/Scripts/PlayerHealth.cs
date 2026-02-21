using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

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

    [Header("Game Over")]
    public GameOverMenu gameOverMenu;

    [Header("Damage Feedback")]
    public float damageFeedbackDuration = 0.2f;
    public Color damageColor = Color.red;
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Vector3 originalPosition;
    private bool isDamaged = false;

    public event Action OnDamaged;

    void Start()
    {
        currentHealth = maxHealth;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        
        originalPosition = transform.localPosition;
        
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
            Die();
        }
        else
        {
            if (!isDamaged)
            {
                StartCoroutine(DamageFeedback());
            }
        }
    }

    public void InstantKill()
    {
        currentHealth = 0;
        OnDamaged?.Invoke();
        
        if (healthSlider != null)
        {
            healthSlider.value = 0;
        }
        
        UpdateHealthBarImages();
        Die();
    }

    void Die()
    {
        Debug.Log("PlayerHealth: Die() called");
        
        if (gameOverMenu != null)
        {
            Debug.Log("PlayerHealth: Calling gameOverMenu.ShowGameOver()");
            gameOverMenu.ShowGameOver();
        }
        else
        {
            Debug.LogWarning("PlayerHealth: gameOverMenu is not assigned! Game Over won't show.");
        }

        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, gameObject.transform.position, Quaternion.identity);
        }

        Debug.Log("Player Died!");
        Destroy(gameObject);
    }

    IEnumerator DamageFeedback()
    {
        isDamaged = true;
        float elapsed = 0f;

        while (elapsed < damageFeedbackDuration)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.Lerp(damageColor, originalColor, elapsed / damageFeedbackDuration);
            }

            transform.localPosition = originalPosition + (Vector3)UnityEngine.Random.insideUnitCircle * shakeMagnitude;
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
        transform.localPosition = originalPosition;

        isDamaged = false;
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
