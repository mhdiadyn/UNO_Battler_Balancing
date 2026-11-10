using UnityEngine;
using TMPro;

// Singleton: hanya ada satu CoinManager, diakses lewat CoinManager.Instance
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    [SerializeField] private TMP_Text coinText;
    [SerializeField] private int firstUpgradeCost = 150; // biaya upgrade level 1 (Bagian A)

    public int Coins { get; private set; }
    private bool tteLogged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // jaga agar tetap satu instance
            return;
        }
        Instance = this;
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        Coins += amount;
        UpdateUI();

        // Catat TTE aktual (Time to Earn) saat coin pertama kali mencapai biaya upgrade
        if (!tteLogged && Coins >= firstUpgradeCost)
        {
            tteLogged = true;
            Debug.Log($"TTE aktual upgrade pertama: {Time.timeSinceLevelLoad / 60f:F2} menit");
        }
    }

    public bool TrySpend(int amount)
    {
        if (amount <= 0 || Coins < amount) return false;
        Coins -= amount;
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        if (coinText != null) coinText.text = $"Coin: {Coins}";
    }
}
