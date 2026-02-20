using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damage = 1;
    public GameObject hitFX;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(hitFX, collision.transform.position, Quaternion.identity);
            collision.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }
    }
}
