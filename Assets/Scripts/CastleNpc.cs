using UnityEngine;
using System.Collections;

public class CastleNpc : MonoBehaviour {
    public float spawnInterval = 3.0f;
    float spawnIntervalElapsed = 0.0f;

    void Update() {
        spawnIntervalElapsed += Time.deltaTime;
        if (spawnIntervalElapsed >= spawnInterval) {
            GetComponent<UnitSpawner>().spawn();

            spawnIntervalElapsed = 0.0f;
        }
    }
}