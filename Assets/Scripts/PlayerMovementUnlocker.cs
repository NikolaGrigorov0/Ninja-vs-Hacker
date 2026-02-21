using UnityEngine;

public class PlayerMovementUnlocker : MonoBehaviour
{
    public static PlayerMovementUnlocker Instance { get; private set; }

    public bool canMoveLeft = false;
    public bool canMoveRight = true;
    public bool canJump = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockLeftMovement()
    {
        canMoveLeft = true;
        Debug.Log("Left movement unlocked! You can now use 'A' and 'D' keys.");
    }

    public void UnlockJump()
    {
        canJump = true;
        Debug.Log("Jump unlocked!");
    }

    public Vector2 FilterMovement(Vector2 input)
    {
        if (!canMoveLeft && input.x < 0)
        {
            input.x = 0;
        }

        if (!canMoveRight && input.x > 0)
        {
            input.x = 0;
        }

        return input;
    }
}
