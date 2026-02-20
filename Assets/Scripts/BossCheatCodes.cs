using UnityEngine;
using UnityEngine.InputSystem;

public class BossCheatCodes : MonoBehaviour
{
    private BossController bossController;
    private PlayerHealth playerHealth;

    void Start()
    {
        bossController = FindFirstObjectByType<BossController>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SpawnBugNow();
        }

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            DamageBossNow();
        }

        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            HealPlayer();
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            KillAllBugs();
        }
    }

    void SpawnBugNow()
    {
        if (bossController != null && bossController.bugPrefab != null && bossController.bugSpawnPoints.Length > 0)
        {
            int index = Random.Range(0, bossController.bugSpawnPoints.Length);
            Instantiate(bossController.bugPrefab, bossController.bugSpawnPoints[index].position, Quaternion.identity);
            Debug.Log($"[CHEAT] Bug spawned at point {index + 1}");
        }
    }

    void DamageBossNow()
    {
        if (bossController != null)
        {
            bossController.DamageBoss();
            Debug.Log("[CHEAT] Boss damaged! Health: " + bossController.GetCurrentBars());
        }
    }

    void HealPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(-1);
            Debug.Log("[CHEAT] Player healed!");
        }
    }

    void KillAllBugs()
    {
        GameObject[] bugs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject bug in bugs)
        {
            Destroy(bug);
        }
        Debug.Log($"[CHEAT] Killed {bugs.Length} bugs");
    }
}
