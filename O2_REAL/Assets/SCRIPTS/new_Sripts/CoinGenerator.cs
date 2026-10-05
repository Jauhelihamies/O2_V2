using UnityEngine;
using TMPro; // Required for TextMesh Pro

public class CoinGenerator : MonoBehaviour
{
    [Header("Generator Settings")]
    [Tooltip("How often to generate coins (in seconds).")]
    public float generationInterval = 3f;

    [Tooltip("How many coins to add each time.")]
    public int coinsPerInterval = 1;

    [Header("UI Reference (Optional)")]
    [Tooltip("Drag your TextMeshPro text here. If left empty, it will try to find one automatically.")]
    public TextMeshProUGUI coinText;

    // Shared across ALL instances of this script so the total count stays synchronized
    private static int totalCoins = 0;
    private float timer;

    void Start()
    {
        timer = generationInterval;

        // If you forgot to drag the text object into the Inspector, try to find one automatically
        if (coinText == null)
        {
            coinText = Object.FindFirstObjectByType<TextMeshProUGUI>();
        }

        // Initialize display
        UpdateUI();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // Update the shared, static total count
            totalCoins += coinsPerInterval;
            UpdateUI();

            timer = generationInterval; // Reset timer
        }
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {totalCoins}";
        }
    }
}