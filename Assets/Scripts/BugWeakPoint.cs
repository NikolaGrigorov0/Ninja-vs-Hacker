using UnityEngine;

public class BugWeakPoint : MonoBehaviour
{
    [Header("Weak Point")]
    public int hitsRequired = 5;
    public float timeLimit = 7f;

    private int currentHits = 0;
    private float timer;
    private bool active = true;

    private BossController boss;
    private SpriteRenderer sr;

    [Header("Damage Sprites")]
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;

    void Start()
    {
        timer = timeLimit;
        boss = FindFirstObjectByType<BossController>();
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = stage1;
    }

    void Update()
    {
        if (!active) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Fail();
        }
    }

    public void TakeHit()
    {
        if (!active) return;

        currentHits++;

        UpdateSprite();

        if (currentHits >= hitsRequired)
        {
            Success();
        }
    }

    void UpdateSprite()
    {
        if (currentHits >= 4)
            sr.sprite = stage3;
        else if (currentHits >= 2)
            sr.sprite = stage2;
        else
            sr.sprite = stage1;
    }

    void Success()
    {
        active = false;
        boss.DamageBoss();
        Destroy(gameObject);
    }

    void Fail()
    {
        active = false;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TakeHit();
        }
    }
}