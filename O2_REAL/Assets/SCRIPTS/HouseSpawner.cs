using System.Collections;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [Header("Setup")]
    public GameObject npcPrefab; // Drag your NPC Prefab here

    [Header("Random Spawning Settings")]
    public float minSpawnTime = 8f;
    public float maxSpawnTime = 12f;

    public bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnNPCRoutine());
    }

    IEnumerator SpawnNPCRoutine()
    {
        while (true)
        {
            // Pick a new random time for this specific wait cycle
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
        // Spawns the NPC at the house's position
        Instantiate(npcPrefab, transform.position, Quaternion.identity);
    }
}