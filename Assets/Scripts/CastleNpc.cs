using UnityEngine;
using System.Collections;

public class CastleNpc : MonoBehaviour {
    public float spawnInterval = 3.0f;
    float spawnIntervalElapsed = 0.0f;

    void Update() {
        spawnIntervalElapsed += Time.deltaTime;
        if (spawnIntervalElapsed >= spawnInterval) {
            // Ошибка: вражеский замок создаёт солдата каждые 3 секунды без конца.
            // Через пару минут их сотни. Каждый ещё и ищет цели каждый кадр
            // (см. MoveByNpc), поэтому игра начинает тормозить.
            //
            // Как исправить. Завести максимум, например 5. Перед созданием
            // солдата посчитать уже живых юнитов этого замка и не создавать нового,
            // если максимум достигнут. Когда юнит умирает, место снова свободно.
            GetComponent<UnitSpawner>().spawn();

            spawnIntervalElapsed = 0.0f;
        }
    }
}