using UnityEngine;

public class PushableBox : MonoBehaviour, IInteractable
{
    [Header("Push Settings")]
    public float pushForce = 5f;
    public float pushDistance = 0.5f;
    
    private Rigidbody2D rb;
    private bool isPushing = false;
    private Transform playerTransform;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            PushBox();
        }
    }

    void PushBox()
    {
        if (playerTransform == null) return;

        float direction = Mathf.Sign(transform.position.x - playerTransform.position.x);
        Vector2 pushDirection = new Vector2(direction, 0);
        
        rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        
        Debug.Log("Box pushed!");
    }
}
