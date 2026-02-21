using UnityEngine;

public class PlayerHealthBarDisplay : MonoBehaviour
{
    public GameObject healthBarObject;
    private PlayerHealth playerHealth;
    private bool hasBeenHit = false;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        
        if (healthBarObject != null)
        {
            healthBarObject.SetActive(false);
        }

        if (playerHealth != null)
        {
            playerHealth.OnDamaged += ShowHealthBar;
        }
    }

    void ShowHealthBar()
    {
        if (!hasBeenHit && healthBarObject != null)
        {
            hasBeenHit = true;
            healthBarObject.SetActive(true);
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDamaged -= ShowHealthBar;
        }
    }
}
