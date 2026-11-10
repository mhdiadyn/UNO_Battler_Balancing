using UnityEngine;

// ScriptableObject: satu aset = satu tipe musuh. Angka diambil dari Bagian A (spreadsheet).
[CreateAssetMenu(fileName = "EnemyStats_New", menuName = "UNO Battler/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [Header("Identitas")]
    public string enemyName = "Enemy";
    public Color tint = Color.white;
    [Min(0.1f)] public float visualScale = 1f;

    [Header("Stat dasar (dari Bagian A)")]
    [Min(1f)] public float hp = 100f;
    [Min(0f)] public float damage = 10f;
    [Min(0.1f)] public float speed = 1.5f;       // satuan unit per detik
    [Min(0.05f)] public float cooldown = 1f;     // detik
    [Range(0f, 0.95f)] public float armor = 0.1f; // 0.1 = kurangi damage 10%
    [Min(1)] public int cost = 45;
    [Min(0)] public int coinReward = 10;

    [Header("Hasil hitung otomatis (jangan diisi manual)")]
    [SerializeField] private float dps;
    [SerializeField] private float effectiveHp;
    [SerializeField] private float powerScore;
    [SerializeField] private float powerPerCost;

    // PowerScore = (Dmg / Cooldown) x (HP / (1 - Armor))  -> sama dengan rumus Bagian A
    private void OnValidate()
    {
        dps = damage / cooldown;
        effectiveHp = hp / (1f - armor);
        powerScore = dps * effectiveHp;
        powerPerCost = powerScore / cost;
    }
}
