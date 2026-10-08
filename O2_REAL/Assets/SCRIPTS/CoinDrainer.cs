using UnityEngine;

// Tämä varmistaa, että kyseisellä peliobjektilla on aina AudioSource kiinni
[RequireComponent(typeof(AudioSource))]
public class CoinDrainer : MonoBehaviour
{
    [Header("Drain Settings")]
    public float drainInterval = 3f;
    public int coinsToDrain = 1;



    [SerializeField] private AudioClip stealSound;


    [Range(0f, 1f)]
    [SerializeField] private float soundVolume = 1f;

    private float timer;
    public AudioSource audioSourcex;

    void Start()
    {
        timer = drainInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {

            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.RemoveCoins(coinsToDrain);

                if (audioSourcex != null && stealSound != null)
                {
                    audioSourcex.PlayOneShot(stealSound, soundVolume);
                }
            }

            timer = drainInterval;
        }
    }
}