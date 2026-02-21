using UnityEngine;

public class EnemyGrowthTrigger : MonoBehaviour
{
    [Header("Settings")]
    public GameObject enemyToGrow;
    public float growthMultiplier = 3f;
    
    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;

        if (collision.CompareTag("Player"))
        {
            GrowEnemy();
        }
    }

    void GrowEnemy()
    {
        hasTriggered = true;

        if (enemyToGrow != null)
        {
            Vector3 currentScale = enemyToGrow.transform.localScale;
            enemyToGrow.transform.localScale = currentScale * growthMultiplier;
            Debug.Log($"Enemy grew {growthMultiplier}x bigger!");
        }
    }
}
