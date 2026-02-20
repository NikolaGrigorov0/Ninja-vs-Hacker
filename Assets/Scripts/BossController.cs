using UnityEngine;
using System;

public class BossController : MonoBehaviour
{
    public int maxBars = 4;
    private int currentBars;

    public GameObject bugPrefab;
    public Transform[] bugSpawnPoints;

    public float bugSpawnInterval = 30f;

    private float spawnTimer;

    public event Action<int> OnHealthChanged;

    void Start()
    {
        currentBars = maxBars;
        spawnTimer = bugSpawnInterval;
        OnHealthChanged?.Invoke(currentBars);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnBug();
            spawnTimer = bugSpawnInterval;
        }
    }

    void SpawnBug()
    {
        if (bugPrefab == null || bugSpawnPoints == null || bugSpawnPoints.Length == 0) return;

        int index = UnityEngine.Random.Range(0, bugSpawnPoints.Length);
        Instantiate(bugPrefab, bugSpawnPoints[index].position, Quaternion.identity);
    }

    public void DamageBoss()
    {
        currentBars--;

        Debug.Log("Boss HP bars left: " + currentBars);

        OnHealthChanged?.Invoke(currentBars);

        CameraShake cam = Camera.main?.GetComponent<CameraShake>();
        if (cam != null)
        {
            cam.Shake(0.3f, 0.4f);
        }

        if (currentBars <= 0)
        {
            Die();
        }
    }

    public int GetCurrentBars()
    {
        return currentBars;
    }

    public float GetNextBugSpawnTime()
    {
        return spawnTimer;
    }

    void Die()
    {
        Debug.Log("Boss Defeated!");
        Destroy(gameObject);
    }
}