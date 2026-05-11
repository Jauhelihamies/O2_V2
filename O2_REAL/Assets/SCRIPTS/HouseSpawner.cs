using System.Collections;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [Header("Setup")]
    public GameObject npcPrefab;

    [Header("Random Spawning Settings")]
    public float minSpawnTime = 8f;
    public float maxSpawnTime = 12f;
    public bool isSpawning = true;

    [Header("House Juice (Bouncing)")]
    public float bounceSpeed = 3f;      // How fast it breathes
    public float squashAmount = 0.1f;   // How much it stretches
    public float tiltAmount = 2f;       // How much it leans left/right

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
        StartCoroutine(SpawnNPCRoutine());
    }

    void Update()
    {
        // Procedural "Breathing" animation
        // Mathf.Sin creates a smooth wave between -1 and 1
        float bounce = Mathf.Sin(Time.time * bounceSpeed);

        // 1. Stretch and Squash (Y shrinks while X grows)
        transform.localScale = new Vector3(
            initialScale.x + (bounce * squashAmount),
            initialScale.y - (bounce * squashAmount),
            initialScale.z);

        // 2. Gentle Tilt (Rotation)
        transform.rotation = Quaternion.Euler(0, 0, bounce * tiltAmount);
    }

    IEnumerator SpawnNPCRoutine()
    {
        while (true)
        {
            float randomWait = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomWait);

            if (isSpawning && npcPrefab != null)
            {
                SpawnNPC();
            }
        }
    }

    void SpawnNPC()
    {
        Instantiate(npcPrefab, transform.position, Quaternion.identity);
    }
}