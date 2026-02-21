using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"SwordHitBox hit: {collision.gameObject.name}, Tag: {collision.tag}");
        
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy detected! Attempting damage...");
            
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Damaged enemy health!");
            }
            
            BugWeakPoint bugWeakPoint = collision.GetComponent<BugWeakPoint>();
            if (bugWeakPoint != null)
            {
                bugWeakPoint.TakeHit();
                Debug.Log("Hit bug weak point!");
            }
        }
    }
}
