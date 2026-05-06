using UnityEngine;

public class BouncyNPC : MonoBehaviour
{
    [Header("Visuals")]
    public Sprite secondSprite;
    public int newSortingOrder = -1;
    private SpriteRenderer spriteRenderer;
    private bool isFrozen = false;
    private Collider2D npcCollider;

    [Header("Audio - Click (Death)")]
    public AudioClip[] clickSounds;

    [Header("Audio - Ambient (Wandering)")]
    public AudioClip[] ambientSounds;
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
        initialScale = transform.localScale;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        PickNewDirection();
        SetNextAmbientTime();
    }

    void Update()
    {
        if (isFrozen) return;

        // 1. Movement
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= changeDirTime) { PickNewDirection(); timer = 0; }

        // 2. Ambient Sound Timer (Only plays if NOT already playing a sound)
        ambientTimer -= Time.deltaTime;
        if (ambientTimer <= 0)
        {
            if (!audioSource.isPlaying) // Check to prevent overlapping
            {
                PlayAmbientSound();
            }
            SetNextAmbientTime();
        }

        // 3. Procedural Bounce/Tilt
        float bounce = Mathf.Sin(Time.time * bounceSpeed);
        transform.localScale = new Vector3(initialScale.x + (bounce * squashAmount), initialScale.y - (bounce * squashAmount), initialScale.z);
        transform.rotation = Quaternion.Euler(0, 0, bounce * tiltAmount);
    }

    void PlayAmbientSound()
    {
        if (ambientSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, ambientSounds.Length);
            audioSource.clip = ambientSounds[randomIndex];
            audioSource.Play();
        }
    }

    void SetNextAmbientTime()
    {
        ambientTimer = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
    }

    public void HandleClick()
    {
        if (isFrozen) return;

        // Immediately stop any ambient sound that is currently playing
        audioSource.Stop();

        if (secondSprite != null)
        {
            spriteRenderer.sprite = secondSprite;
            spriteRenderer.sortingOrder = newSortingOrder;
        }

        // Play the click sound
        if (clickSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, clickSounds.Length);
            audioSource.PlayOneShot(clickSounds[randomIndex]);
        }

        isFrozen = true; // This prevents Update() from running movement and ambient timers
        if (npcCollider != null) npcCollider.enabled = false;

        transform.localScale = initialScale;
        transform.rotation = Quaternion.identity;
    }

    void PickNewDirection()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}