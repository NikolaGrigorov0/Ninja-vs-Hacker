using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float knockbackForce = 3f;

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy taking " + damage.ToString() + " damage");
        StartCoroutine(Flash());
        Knockback();
        currentHealth -= damage;
        if (currentHealth <= 0)
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
        Vector2 dir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        rb.AddForce(-dir * knockbackForce, ForceMode2D.Impulse);
    }
}
