using UnityEngine;

// Pasang di prefab Enemy_Base. Tiap Prefab Variant hanya mengganti slot "stats".
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    [SerializeField] private float endX = -9.5f; // posisi "base" di kiri layar

    private float currentHp;
    private bool isDead;

    private void Start()
    {
        if (stats == null)
        {
            Debug.LogError($"{name}: slot Stats belum diisi!", this);
            enabled = false;
            return;
        }

        currentHp = stats.hp;
        transform.localScale = Vector3.one * stats.visualScale;

        var rend = GetComponent<Renderer>();
        if (rend != null) rend.material.color = stats.tint;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * stats.speed * Time.deltaTime, Space.World);

        if (transform.position.x <= endX)
        {
            Debug.Log($"{stats.enemyName} lolos ke base");
            Destroy(gameObject);
        }
    }

    // Armor mengurangi damage: sesuai konsep Effective HP = HP / (1 - Armor)
    public void TakeHit(float rawDamage)
    {
        if (isDead || stats == null) return;

        currentHp -= rawDamage * (1f - stats.armor);
        if (currentHp <= 0f) Die();
    }

    private void Die()
    {
        isDead = true;
        if (CoinManager.Instance != null)
            CoinManager.Instance.AddCoins(stats.coinReward);
        Destroy(gameObject);
    }
}
