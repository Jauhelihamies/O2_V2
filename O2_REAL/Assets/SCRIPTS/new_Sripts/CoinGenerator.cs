using UnityEngine;
using TMPro; // Required for TextMesh Pro

public class CoinGenerator : MonoBehaviour
{
    [Header("Generator Settings")]
    [Tooltip("How often to generate coins (in seconds).")]
    public float generationInterval = 3f;

    [Tooltip("How many coins to add each time.")]
    public int coinsPerInterval = 1;

    [Header("UI Settings")]
    [Tooltip("The Tag assigned to your Coin Text object in the scene.")]
    public string coinTextTag = "CoinText";

    // Text reference found automatically at runtime
    private TextMeshProUGUI coinText;

    // Shared across ALL instances of this script so the total count stays synchronized
    private static int totalCoins = 0;
    private float timer;

    void Start()
    {
        timer = generationInterval;

        // Etsitään tekstikenttä automaattisesti skenestä Tagin perusteella
        GameObject textObject = GameObject.FindWithTag(coinTextTag);
        if (textObject != null)
        {
            coinText = textObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning($"CoinGenerator: Kohdetta tagilla '{coinTextTag}' ei löytynyt skenestä!");
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