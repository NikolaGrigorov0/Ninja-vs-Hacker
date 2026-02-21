using UnityEngine;

public class ScrollPickup : MonoBehaviour
{
    [Header("Settings")]
    public string unlockMessage = "Movement Unlocked!\nYou can now move Left (A) and Right (D)!";
    public float messageDisplayTime = 3f;
    public GameObject doorToReveal;

    [Header("Unlock Type")]
    public bool unlockLeftMovement = false;
    public bool unlockJump = false;
    public bool unlockInteract = false;
    public bool unlockAttack = false;

    private bool collected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.CompareTag("Player"))
        {
            CollectScroll();
        }
    }

    void CollectScroll()
    {
        collected = true;

        PlayerMovementUnlocker unlocker = FindFirstObjectByType<PlayerMovementUnlocker>();
        if (unlocker != null)
        {
            if (unlockLeftMovement)
            {
                unlocker.UnlockLeftMovement();
            }
            
            if (unlockJump)
            {
                unlocker.UnlockJump();
            }
            
            if (unlockInteract)
            {
                unlocker.UnlockInteract();
            }
            
            if (unlockAttack)
            {
                unlocker.UnlockAttack();
            }
        }

        UnlockMessageUI messageUI = FindFirstObjectByType<UnlockMessageUI>();
        if (messageUI != null)
        {
            messageUI.ShowMessage(unlockMessage, messageDisplayTime);
        }

        if (doorToReveal != null)
        {
            doorToReveal.SetActive(true);
            Debug.Log("Door revealed!");
        }

        Destroy(gameObject);
    }
}
