using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damage = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }
    }
}
