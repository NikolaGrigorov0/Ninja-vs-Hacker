using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [Header("Health Bar Images")]
    public Image[] healthBars;

    private BossController bossController;

    void Start()
    {
        bossController = FindFirstObjectByType<BossController>();
        if (bossController != null)
        {
            bossController.OnHealthChanged += UpdateHealthBars;
            UpdateHealthBars(bossController.GetCurrentBars());
        }
    }

    void UpdateHealthBars(int currentBars)
    {
        for (int i = 0; i < healthBars.Length; i++)
        {
            if (healthBars[i] != null)
            {
                healthBars[i].enabled = i < currentBars;
            }
        }
    }

    void OnDestroy()
    {
        if (bossController != null)
        {
            bossController.OnHealthChanged -= UpdateHealthBars;
        }
    }
}
