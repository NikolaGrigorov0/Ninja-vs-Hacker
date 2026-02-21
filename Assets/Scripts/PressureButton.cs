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
        if (isPressed) return;

        if (collision.CompareTag("Untagged") || collision.GetComponent<PushableBox>() != null)
        {
            PressButton();
        }
    }

    void PressButton()
    {
        if (isPressed) return;

        isPressed = true;

        if (spriteRenderer != null && pressedSprite != null)
        {
            spriteRenderer.sprite = pressedSprite;
        }

        if (scrollToReveal != null)
        {
            scrollToReveal.SetActive(true);
            Debug.Log("Button pressed! Scroll revealed!");
        }
    }
}
