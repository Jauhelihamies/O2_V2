using UnityEngine;

public class NpcMoneyProducer : MonoBehaviour
{

    public float productionInterval = 3f;


    public int coinsPerInterval = 1;

    private float timer;
    private bool isDestroyed = false; // Lippu, jolla estet‰‰n rahan tulo tuhottaessa

    void Start()
    {
        timer = productionInterval;
    }

    void Update()
    {
 
        if (isDestroyed) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {

            if (CoinManager.Instance != null && gameObject != null)
            {
                CoinManager.Instance.AddCoins(coinsPerInterval);
            }

            timer = productionInterval; // Nollataan ajastin
        }
    }


    private void OnDestroy()
    {
        isDestroyed = true;
    }
}