using UnityEngine;

public class BugWeakPoint : MonoBehaviour
{
    [Header("Weak Point")]
    public int hitsRequired = 3;
    public float timeLimit = 8f;

    private int currentHits = 0;
    private float startTime;
    private bool active = true;

    private BossController boss;
    private SpriteRenderer sr;

    [Header("Damage Sprites")]
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;

    void Start()
    {
        startTime = Time.time;
        boss = FindFirstObjectByType<BossController>();
        sr = GetComponent<SpriteRenderer>();

        if (sr != null && stage1 != null)
        {
            sr.sprite = stage1;
        }
    }

    void Update()
    {
        if (!active) return;

        if (Time.time - startTime >= timeLimit)
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
        if (sr == null) return;

        if (currentHits >= 3 && stage3 != null)
            sr.sprite = stage3;
        else if (currentHits == 2 && stage2 != null)
            sr.sprite = stage2;
        else if (stage1 != null)
            sr.sprite = stage1;
    }

    void Success()
    {
        active = false;
        if (boss != null)
        {
            boss.DamageBoss();
        }
        Destroy(gameObject);
    }

    void Fail()
    {
        active = false;
        Destroy(gameObject);
    }
}