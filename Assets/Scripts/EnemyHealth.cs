using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float knockbackForce = 3f;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        Debug.Log("Enemy taking " + damage.ToString() + " damage");
        StartCoroutine(Flash());
        Knockback();
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        SkeletonAI skeletonAI = GetComponent<SkeletonAI>();
        if (skeletonAI != null)
        {
            skeletonAI.Die();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Flash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    void Knockback()
    {
        if (rb != null)
        {
            Vector2 dir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.AddForce(-dir * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
