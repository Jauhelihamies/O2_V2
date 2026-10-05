using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    // Singleton-viittaus
    public static CoinManager Instance { get; private set; }

    [Header("UI Settings")]
    [Tooltip("The Tag assigned to your Coin Text object in the scene.")]
    public string coinTextTag = "CoinText";
    public int Goal = 100;
    private TextMeshProUGUI coinText;


    private int totalCoins = 0;

    private void Awake()
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

    void Start()
    {

        GameObject textObject = GameObject.FindWithTag(coinTextTag);
        if (textObject != null)
        {
            coinText = textObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {

        }

        UpdateUI();
    }
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {totalCoins}";
            if (totalCoins >= Goal)
            {
                SceneManager.LoadScene(3);
            }
        }
    }
    public bool HasEnoughCoins(int amount)
    {
        return totalCoins >= amount;
    }

    // Vähentää kolikot ja päivittää käyttöliittymän
    public void RemoveCoins(int amount)
    {
        totalCoins -= amount;
        if (totalCoins < 0) totalCoins = 0; // Varmistus, ettei mene miinukselle
        UpdateUI();
    }
}