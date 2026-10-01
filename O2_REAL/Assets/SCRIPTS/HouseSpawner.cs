using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableNPC
    {
        public GameObject npcPrefab;

        public int spawnWeight;
    }

    [Header("NPC Settings")]
    [SerializeField] private List<SpawnableNPC> npcList = new List<SpawnableNPC>();
    [SerializeField] private float minSpawnTime = 8f;
    [SerializeField] private float maxSpawnTime = 12f;
    public bool isSpawning = true;

    [Header("Juice & Animation Settings")]
    [SerializeField] private float bounceSpeed = 3f;
    [SerializeField] private float squashAmount = 0.1f;
    [SerializeField] private float tiltAmount = 2f;

    [Header("References")]


    private Vector3 initialScale;
    private int totalWeight;

    void Start()
    {
        initialScale = transform.localScale;

        // Välimuistitetaan kokonaispaino kerran käynnistyksessä
        CalculateTotalWeight();


        StartCoroutine(SpawnNPCRoutine());
    }

    void Update()
    {
        AnimateHouse();
    }

    private void AnimateHouse()
    {
        float bounce = Mathf.Sin(Time.time * bounceSpeed);

        transform.localScale = new Vector3(
            initialScale.x + (bounce * squashAmount),
            initialScale.y - (bounce * squashAmount),
            initialScale.z);

        transform.rotation = Quaternion.Euler(0, 0, bounce * tiltAmount);
    }

    private IEnumerator SpawnNPCRoutine()
    {
        while (true)
        {
            float randomWait = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomWait); // Huom: Voit käyttää myös WaitForSecondsRealtime jos peli pausetaan

            if (isSpawning && npcList.Count > 0)
            {
                SpawnNPC();


            }
        }
    }

    private void CalculateTotalWeight()
    {
        totalWeight = 0;
        foreach (var npc in npcList)
        {
            totalWeight += Mathf.Max(0, npc.spawnWeight);
        }
    }

    private void SpawnNPC()
    {
        if (totalWeight <= 0) return;

        int rolledValue = Random.Range(0, totalWeight);
        int currentWeightCounter = 0;

        foreach (var npc in npcList)
        {
            currentWeightCounter += npc.spawnWeight;
            if (rolledValue < currentWeightCounter)
            {
                if (npc.npcPrefab != null)
                {
                    Instantiate(npc.npcPrefab, transform.position, Quaternion.identity);
                }
                break;
            }
        }
    }
}