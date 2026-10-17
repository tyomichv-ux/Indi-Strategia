using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlePlayer : MonoBehaviour
{

    public float spawnInterval = 3.0f;
    float spawnIntervalElapsed = 0.0f;
    public int maxUnits = 5;

    List<GameObject> aliveUnits = new List<GameObject>();

    void Update()
    {
        // Убираем мёртвых юнитов из списка
        aliveUnits.RemoveAll(u => u == null);

        spawnIntervalElapsed += Time.deltaTime;

        if (spawnIntervalElapsed >= spawnInterval)
        {
            // Спавним только если живых меньше лимита
            if (aliveUnits.Count < maxUnits)
            {
                GameObject unit = GetComponent<UnitSpawner>().spawn();
                if (unit != null) aliveUnits.Add(unit);
            }

            // Сброс таймера
            spawnIntervalElapsed = 0.0f;
        }
    }
}