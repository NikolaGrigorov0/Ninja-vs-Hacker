using UnityEngine;
using UnityEngine.UI;

public class BossNextBugTimer : MonoBehaviour
{
    public Text timerText;
    public Image fillImage;
    
    private BossController bossController;
    private float nextSpawnTime;

    void Start()
    {
        bossController = FindFirstObjectByType<BossController>();
        if (bossController != null)
        {
            nextSpawnTime = Time.time + bossController.bugSpawnInterval;
        }
    }

    void Update()
    {
        if (bossController == null) return;

        float timeRemaining = Mathf.Max(0, nextSpawnTime - Time.time);
        
        if (timeRemaining <= 0)
        {
            nextSpawnTime = Time.time + bossController.bugSpawnInterval;
        }

        if (timerText != null)
        {
            timerText.text = $"Next Bug: {timeRemaining:F1}s";
        }

        if (fillImage != null)
        {
            fillImage.fillAmount = 1f - (timeRemaining / bossController.bugSpawnInterval);
        }
    }
}
