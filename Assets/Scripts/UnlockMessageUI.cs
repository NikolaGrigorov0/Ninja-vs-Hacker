using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnlockMessageUI : MonoBehaviour
{
    public Text messageText;
    public GameObject messagePanel;

    void Start()
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
    }

    public void ShowMessage(string message, float duration)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }

        if (messagePanel != null)
        {
            messagePanel.SetActive(true);
        }

        StartCoroutine(HideMessageAfterDelay(duration));
    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
    }
}
