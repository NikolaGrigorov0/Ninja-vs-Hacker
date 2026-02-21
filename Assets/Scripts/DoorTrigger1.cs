using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DoorTrigger1 : MonoBehaviour
{
    [Header("Door Settings")]
    public string nextSceneName = "SceneLevel2,";
    public Sprite closedDoorSprite;
    public Sprite openedDoorSprite;

    [Header("Interaction")]
    public bool requireInput = true;
    public string interactKey = "E";
    private AudioSource audioSource;

    private bool playerNearby = false;
    private bool doorOpened = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
         audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null && closedDoorSprite != null)
        {
            spriteRenderer.sprite = closedDoorSprite;
        }
    }

    void Update()
    {
        if (playerNearby && requireInput && !doorOpened)
        {
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                            audioSource.Play();

                OpenDoor();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;

            if (!requireInput)
            {
                            audioSource.Play();

                OpenDoor();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    void OpenDoor()
    {
        if (doorOpened) return;
            audioSource.Play();

        doorOpened = true;

        if (spriteRenderer != null && openedDoorSprite != null)
        {
            spriteRenderer.sprite = openedDoorSprite;
        }

        LoadNextLevel();
    }

    void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("DoorTrigger: Next scene name not set!");
        }
    }
}
