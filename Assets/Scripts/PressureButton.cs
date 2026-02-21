using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [Header("Button Settings")]
    public Sprite unpressedSprite;
    public Sprite pressedSprite;
    public GameObject scrollToReveal;
    
    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null && unpressedSprite != null)
        {
            spriteRenderer.sprite = unpressedSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Button triggered by: {collision.gameObject.name}, Tag: {collision.tag}");
        
        if (isPressed) return;

        if (collision.CompareTag("Untagged") || collision.GetComponent<PushableBox>() != null)
        {
            Debug.Log("Pressing button!");
            PressButton();
        }
    }

    void PressButton()
    {
        if (isPressed) return;

        Debug.Log("PressButton() called!");
        isPressed = true;

        if (spriteRenderer != null && pressedSprite != null)
        {
            spriteRenderer.sprite = pressedSprite;
            Debug.Log("Button sprite changed to pressed!");
        }
        else
        {
            Debug.LogWarning($"Sprite change failed! SpriteRenderer: {spriteRenderer}, PressedSprite: {pressedSprite}");
        }

        if (scrollToReveal != null)
        {
            scrollToReveal.SetActive(true);
            Debug.Log($"Scroll revealed! Scroll name: {scrollToReveal.name}, Active: {scrollToReveal.activeSelf}");
        }
        else
        {
            Debug.LogError("ScrollToReveal is null! Cannot reveal scroll.");
        }
    }
}
