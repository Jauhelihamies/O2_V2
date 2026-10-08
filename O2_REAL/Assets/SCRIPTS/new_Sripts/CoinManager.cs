using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class CoinManager : MonoBehaviour
{

    public static CoinManager Instance { get; private set; }
    public string coinTextTag = "CoinText";
    public int Goal = 100;
    public float winDelay = 3f;

    private TextMeshProUGUI coinText;
    private int totalCoins = 0;
    private bool isWinning = false;

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


            if (totalCoins >= Goal && !isWinning)
            {
                isWinning = true;
                StartCoroutine(LoadWinSceneWithDelay());
            }
        }
    }

    private IEnumerator LoadWinSceneWithDelay()
    {

        yield return new WaitForSeconds(winDelay);

        SceneManager.LoadScene(3);
    }

    public bool HasEnoughCoins(int amount)
    {
        return totalCoins >= amount;
    }

    public void RemoveCoins(int amount)
    {
        totalCoins -= amount;
        if (totalCoins < 0) totalCoins = 0;
        UpdateUI();
    }
}