using UnityEngine;
using TMPro;

public class BossDebugUI : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private BossController bossController;

    void Start()
    {
        bossController = FindFirstObjectByType<BossController>();
    }

    void Update()
    {
        if (debugText != null && bossController != null)
        {
            float timeUntilNextBug = bossController.bugSpawnInterval - (Time.time % bossController.bugSpawnInterval);
            debugText.text = $"Boss Health: {bossController.GetCurrentBars()}/4\nNext Bug in: {timeUntilNextBug:F1}s";
        }
    }
}
