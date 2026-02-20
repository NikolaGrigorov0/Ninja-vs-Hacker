using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public Slider healthSlider;

    public GameObject explosionVFX;

    public Animator animator;
    public Rigidbody2D rb;
    public float knockbackForce = 5f;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);

            Vector2 hitDir = transform.position.x < collision.transform.position.x ? Vector2.left : Vector2.right;
            rb.AddForce(new Vector2(hitDir.x * knockbackForce, knockbackForce), ForceMode2D.Impulse);

            animator.SetTrigger("Hurt");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            //Spawn Explosion VFX 
            Instantiate(explosionVFX, gameObject.transform.position, Quaternion.identity);
            Debug.Log("Player Died!");
            Destroy(gameObject);
        }
    }
}
