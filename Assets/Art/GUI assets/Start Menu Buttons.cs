using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Call this on Start button click
    public void StartGame()
    {
        // Make sure your gameplay scene name matches
        SceneManager.LoadScene("GameScene");
    }

    // Call this on Quit button click
    public void QuitGame()
    {
        // Works in built game, not in editor
        Application.Quit();

        // Optional: show message in editor for testing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
