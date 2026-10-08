using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public float generationInterval = 3f;
    public int coinsPerInterval = 1;

    private float timer;

    void Start()
    {

        timer = generationInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {

            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.AddCoins(coinsPerInterval);
            }
            else
            {

            }

            timer = generationInterval; 
        }
    }
}