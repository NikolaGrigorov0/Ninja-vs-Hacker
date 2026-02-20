using UnityEngine;

public class BugSpawnIndicator : MonoBehaviour
{
    public GameObject indicatorPrefab;
    public Transform[] spawnPoints;

    public void ShowSpawnIndicator(Transform spawnPoint)
    {
        if (indicatorPrefab != null && spawnPoint != null)
        {
            GameObject indicator = Instantiate(indicatorPrefab, spawnPoint.position, Quaternion.identity);
            Destroy(indicator, 1f);
        }
    }
}
