using UnityEngine;

public class BossBlastAnimator : MonoBehaviour
{
    [Header("Animation Sprites")]
    public Sprite[] animationFrames;
    
    [Header("Settings")]
    public float frameRate = 12f;
    
    private SpriteRenderer spriteRenderer;
    private float frameTimer;
    private int currentFrame;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (animationFrames == null || animationFrames.Length == 0)
        {
            Debug.LogWarning("BossBlastAnimator: No animation frames assigned!");
        }
    }

    void Update()
    {
        if (animationFrames == null || animationFrames.Length == 0 || spriteRenderer == null)
            return;

        frameTimer += Time.deltaTime;
        
        float frameDuration = 1f / frameRate;
        
        if (frameTimer >= frameDuration)
        {
            frameTimer = 0f;
            currentFrame = (currentFrame + 1) % animationFrames.Length;
            spriteRenderer.sprite = animationFrames[currentFrame];
        }
    }
}
