using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private int damageAmount;

    public float lifetime = 5f;

    public void Initialize(Vector2 dir, float spd)
    {
        direction = dir;
        speed = spd;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                int damage = Mathf.CeilToInt(playerHealth.maxHealth / 5f);
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
