using UnityEngine;

public class BossController : MonoBehaviour
{
    public int maxBars = 4;
    private int currentBars;

    public GameObject bugPrefab;
    public Transform[] bugSpawnPoints;

    public float bugSpawnInterval = 30f;

    private float timer;

    void Start()
    {
        currentBars = maxBars;
        timer = bugSpawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnBug();
            timer = bugSpawnInterval;
        }
    }

    void SpawnBug()
    {
        int index = Random.Range(0, bugSpawnPoints.Length);
        Instantiate(bugPrefab, bugSpawnPoints[index].position, Quaternion.identity);
    }

    public void DamageBoss()
    {
        currentBars--;

        Debug.Log("Boss HP bars left: " + currentBars);

        if (currentBars <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss Defeated!");
        Destroy(gameObject);
    }
}