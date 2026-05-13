using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{

    // A custom data structure to pair a Prefab with its custom weight
    [System.Serializable]
    public struct SpawnableNPC
    {
        public GameObject npcPrefab;
        [Tooltip("Higher weight = more common. Lower weight = rarer. Set to 1 for ultra-rare, 100 for common.")]
        public int spawnWeight;
    }

    [Header("NPC Spawn List")]
    public List<SpawnableNPC> npcList = new List<SpawnableNPC>();

    [Header("Random Spawning Settings")]
    public float minSpawnTime = 8f;
    public float maxSpawnTime = 12f;
    public bool isSpawning = true;

    [Header("House Juice (Bouncing)")]
    public float bounceSpeed = 3f;
    public float squashAmount = 0.1f;
    public float tiltAmount = 2f;

    private Vector3 initialScale;

    void Start()
    {

        initialScale = transform.localScale;
        StartCoroutine(SpawnNPCRoutine());
    }

    void Update()
    {
        float bounce = Mathf.Sin(Time.time * bounceSpeed);

        transform.localScale = new Vector3(
            initialScale.x + (bounce * squashAmount),
            initialScale.y - (bounce * squashAmount),
            initialScale.z);

        transform.rotation = Quaternion.Euler(0, 0, bounce * tiltAmount);
    }

    IEnumerator SpawnNPCRoutine()
    {
        while (true)
        {
            float randomWait = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomWait);

            if (isSpawning && npcList.Count > 0)
            {
                SpawnNPC();
                Rotate Generator = Object.FindAnyObjectByType<Rotate>();
                Generator.GetNewNpc();

            }
        }
    }

    void SpawnNPC()
    {
        // 1. Calculate the total weight of all NPCs combined
        int totalWeight = 0;
        foreach (var npc in npcList)
        {
            totalWeight += Mathf.Max(0, npc.spawnWeight); // Prevent negative numbers
        }

        if (totalWeight <= 0) return;

        // 2. Roll a random number between 0 and the total combined weight
        int rolledValue = Random.Range(0, totalWeight);

        // 3. Figure out which NPC the roll landed on
        GameObject prefabToSpawn = null;
        int currentWeightCounter = 0;

        foreach (var npc in npcList)
        {
            currentWeightCounter += npc.spawnWeight;
            if (rolledValue < currentWeightCounter)
            {
                prefabToSpawn = npc.npcPrefab;
                break;
            }
        }

        // 4. Instantiate the winning NPC
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
    }
}