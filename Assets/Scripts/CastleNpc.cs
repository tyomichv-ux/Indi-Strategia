using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleNpc : MonoBehaviour {
    public float spawnInterval = 3.0f;
    float spawnIntervalElapsed = 0.0f;
    public int maxUnits = 5;

    List<GameObject> aliveUnits = new List<GameObject>();//список живых юнитов замка

    void Update() {

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