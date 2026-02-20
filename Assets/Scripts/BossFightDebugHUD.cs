using UnityEngine;
using UnityEngine.InputSystem;

public class BossFightDebugHUD : MonoBehaviour
{
    public bool showDebugInfo = true;

    private BossController bossController;
    private PlayerHealth playerHealth;
    private GUIStyle style;

    void Start()
    {
        bossController = FindFirstObjectByType<BossController>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();

        style = new GUIStyle();
        style.fontSize = 16;
        style.normal.textColor = Color.white;
        style.padding = new RectOffset(10, 10, 10, 10);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.f1Key.wasPressedThisFrame)
        {
            showDebugInfo = !showDebugInfo;
        }
    }

    void OnGUI()
    {
        if (!showDebugInfo) return;

        GUI.Box(new Rect(10, 10, 250, 180), "");

        float y = 20;
        GUI.Label(new Rect(20, y, 230, 25), "=== BOSS FIGHT DEBUG ===", style);
        y += 30;

        if (bossController != null)
        {
            GUI.Label(new Rect(20, y, 230, 25), $"Boss Health: {bossController.GetCurrentBars()}/4", style);
            y += 25;
            
            float nextBug = bossController.GetNextBugSpawnTime();
            GUI.Label(new Rect(20, y, 230, 25), $"Next Bug: {nextBug:F1}s", style);
            y += 25;
        }
        else
        {
            GUI.Label(new Rect(20, y, 230, 25), "Boss: DEFEATED!", style);
            y += 50;
        }

        if (playerHealth != null)
        {
            int currentHealth = Mathf.Max(0, playerHealth.maxHealth);
            GUI.Label(new Rect(20, y, 230, 25), $"Player Health: {currentHealth}/{playerHealth.maxHealth}", style);
            y += 25;
        }
        else
        {
            GUI.Label(new Rect(20, y, 230, 25), "Player: DEFEATED!", style);
            y += 25;
        }

        int bugCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        GUI.Label(new Rect(20, y, 230, 25), $"Active Bugs: {bugCount}", style);
        y += 25;

        GUI.Label(new Rect(20, y, 230, 25), "Press F1 to toggle", style);
    }
}
