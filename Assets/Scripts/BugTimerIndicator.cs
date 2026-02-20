using UnityEngine;

public class BugTimerIndicator : MonoBehaviour
{
    private BugWeakPoint bugWeakPoint;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        bugWeakPoint = GetComponent<BugWeakPoint>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void Update()
    {
        if (bugWeakPoint == null || spriteRenderer == null) return;

        float timeRemaining = bugWeakPoint.timeLimit - Time.time;
        float normalizedTime = timeRemaining / bugWeakPoint.timeLimit;

        if (normalizedTime < 0.3f)
        {
            float flashSpeed = 10f;
            float alpha = Mathf.PingPong(Time.time * flashSpeed, 1f);
            Color flashColor = Color.Lerp(Color.red, originalColor, alpha);
            spriteRenderer.color = flashColor;
        }
    }
}
