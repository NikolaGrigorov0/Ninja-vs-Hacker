using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject blastPrefab;
    public Transform firePoint;
    public float attackInterval = 2f;
    public float blastSpeed = 5f;

    [Header("Audio (Optional)")]
    public AudioClip fireSound;
    
    private float attackTimer;
    private Transform player;
    private AudioSource audioSource;

    void Start()
    {
        attackTimer = attackInterval;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                return;
            }
        }

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            FireBlast();
            attackTimer = attackInterval;
        }
    }

    void FireBlast()
    {
        if (blastPrefab == null || firePoint == null) return;

        GameObject blast = Instantiate(blastPrefab, firePoint.position, Quaternion.identity);
        
        Vector2 direction = (player.position - firePoint.position).normalized;
        
        BossProjectile projectile = blast.GetComponent<BossProjectile>();
        if (projectile != null)
        {
            projectile.Initialize(direction, blastSpeed);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        blast.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
}
