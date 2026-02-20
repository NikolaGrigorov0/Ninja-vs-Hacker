using UnityEngine;
using UnityEngine.SceneManagement;

public class BossGameManager : MonoBehaviour
{
    private BossController bossController;
    private PlayerHealth playerHealth;

    public string victoryMessage = "Boss Defeated!";
    public string defeatMessage = "You Died!";

    private bool gameOver = false;

    void Start()
    {
        bossController = FindFirstObjectByType<BossController>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (gameOver) return;

        if (bossController == null)
        {
            Victory();
        }

        if (playerHealth == null || GameObject.FindGameObjectWithTag("Player") == null)
        {
            Defeat();
        }
    }

    void Victory()
    {
        gameOver = true;
        Debug.Log(victoryMessage);
    }

    void Defeat()
    {
        gameOver = true;
        Debug.Log(defeatMessage);
    }

    public void RestartBossFight()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
