using UnityEngine;

public class BouncyNPC : MonoBehaviour
{
    // A custom structure pairing an individual clip with its own volume slider
    [System.Serializable]
    public struct SoundSettings
    {
        public AudioClip clip;
        [Range(0f, 1f)] public float volume; // Individual volume slider from 0 to 1
    }

    // SHARED SCORE COUNTER
    public static int totalNPCsClicked = 0;
    private static bool isAnyAmbientSoundPlaying = false;

    [Header("Visuals")]
    public Sprite secondSprite;
    public int newSortingOrder = -1;
    private SpriteRenderer spriteRenderer;
    private bool isFrozen = false;
    private Collider2D npcCollider;
    private Rigidbody2D rb; // Fysiikkakomponentti törmäyksiä varten

    [Header("Audio - Click (Death)")]
    public SoundSettings[] clickSounds; // List with individual volumes

    [Header("Audio - Ambient (Wandering)")]
    public SoundSettings[] ambientSounds; // List with individual volumes
    public float minTimeBetweenSounds = 3f;
    public float maxTimeBetweenSounds = 8f;
    private float ambientTimer;

    private AudioSource audioSource;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float changeDirTime = 2f;
    private Vector2 moveDirection;
    private float timer;

    [Header("Bounce")]
    public float bounceSpeed = 5f;
    public float squashAmount = 0.2f;
    public float tiltAmount = 10f;
    private Vector3 initialScale;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        npcCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>(); // Haetaan Rigidbody fysiikkaa varten
        initialScale = transform.localScale;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        PickNewDirection();
        SetNextAmbientTime();
        if (HappiManager.Instance != null) HappiManager.Instance.GetNewNpc();
    }

    void Update()
    {
        if (isFrozen) return;

        // 1. Suunnanvaihdon ajastin (Liike on siirretty FixedUpdateen)
        timer += Time.deltaTime;
        if (timer >= changeDirTime)
        {
            PickNewDirection();
            timer = 0;
        }

        // 2. Ambient Sound Timer
        ambientTimer -= Time.deltaTime;
        if (ambientTimer <= 0)
        {
            if (!isAnyAmbientSoundPlaying && !audioSource.isPlaying)
            {
                StartCoroutine(PlayAmbientUnique());
            }
            SetNextAmbientTime();
        }

        // 3. Procedural Bounce
        float bounce = Mathf.Sin(Time.time * bounceSpeed);
        transform.localScale = new Vector3(initialScale.x + (bounce * squashAmount), initialScale.y - (bounce * squashAmount), initialScale.z);
        transform.rotation = Quaternion.Euler(0, 0, bounce * tiltAmount);
    }

    // FixedUpdate suorittaa fysiikkaan liittyvän liikkeen tasaisesti
    void FixedUpdate()
    {
        if (isFrozen)
        {
            if (rb != null) rb.linearVelocity = Vector2.zero;
            return;
        }

        // Liikutetaan hahmoa asettamalla sille tasainen nopeus
        if (rb != null)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    System.Collections.IEnumerator PlayAmbientUnique()
    {
        if (ambientSounds.Length > 0)
        {
            isAnyAmbientSoundPlaying = true;

            // Pick a random SoundSettings element
            int randomIndex = Random.Range(0, ambientSounds.Length);
            SoundSettings selectedSound = ambientSounds[randomIndex];

            if (selectedSound.clip != null)
            {
                audioSource.volume = selectedSound.volume;
                audioSource.clip = selectedSound.clip;
                audioSource.Play();

                yield return new WaitWhile(() => audioSource.isPlaying);
            }

            isAnyAmbientSoundPlaying = false;
        }
    }

    public void HandleClick()
    {
        if (isFrozen) return;
        NpcMoneyProducer moneyProducer = GetComponent<NpcMoneyProducer>();
        if (moneyProducer != null)
        {
            moneyProducer.enabled = false;
        }
        totalNPCsClicked++;
        if (HappiManager.Instance != null) HappiManager.Instance.NpcKilled();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            isAnyAmbientSoundPlaying = false;
        }

        if (secondSprite != null)
        {
            spriteRenderer.sprite = secondSprite;
            spriteRenderer.sortingOrder = newSortingOrder;
        }
        if (clickSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, clickSounds.Length);
            SoundSettings selectedClick = clickSounds[randomIndex];

            if (selectedClick.clip != null)
            {
                audioSource.PlayOneShot(selectedClick.clip, selectedClick.volume);
            }
        }

        isFrozen = true;
        if (npcCollider != null) npcCollider.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero; 

        transform.localScale = initialScale;
        transform.rotation = Quaternion.identity;
    }

    void SetNextAmbientTime() => ambientTimer = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
    void PickNewDirection()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}