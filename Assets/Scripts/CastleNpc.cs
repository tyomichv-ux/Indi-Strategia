using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleNpc : MonoBehaviour
{
    public float spawnInterval = 3.0f;
    float spawnIntervalElapsed = 0.0f;
    public int maxUnits = 5;

    List<GameObject> aliveUnits = new List<GameObject>();

    void Start()
    {
        // 0 = Легко, 1 = Средне, 2 = Сложно
        int difficulty = PlayerPrefs.GetInt("GameDifficulty", 0);

        switch (difficulty)
        {
            case 0: maxUnits = 5; break; // Легко
            case 1: maxUnits = 8; break; // Средне
            case 2: maxUnits = 11; break; // Сложно
            default: maxUnits = 5; break;
        }

        Debug.Log("CastleNpc: сложность " + difficulty + ", maxUnits = " + maxUnits);
    }

    void Update()
    {

        aliveUnits.RemoveAll(u => u == null);

        spawnIntervalElapsed += Time.deltaTime;
        if (spawnIntervalElapsed >= spawnInterval)
        {
            if (aliveUnits.Count < maxUnits)
            {
                GameObject unit = GetComponent<UnitSpawner>().spawn();
                if (unit != null) aliveUnits.Add(unit);
            }
            spawnIntervalElapsed = 0.0f;
        }
    }
}   