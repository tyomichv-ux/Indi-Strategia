using UnityEngine;
using System.Collections;

public class MoveByPlayer : MonoBehaviour {
	void Update () {
        if (Input.GetMouseButtonDown(1) && isSelected()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Ошибка: правый клик по солдату или замку отправляет отряд не на землю,
            // а в точку на этом объекте. Точка часто висит в воздухе, над сеткой
            // дорог (NavMesh). Юнит идёт криво или в консоли появляется ошибка,
            // что точку нельзя поставить на сетку.
            //
            // Почему. Луч из камеры останавливается на первом предмете.
            // Проверки «это земля?» здесь нет.
            //
            // Как исправить. Двигать юнита только если луч попал в землю.
            // Надёжнее дать земле отдельный слой и сказать лучу смотреть
            // только в него, чтобы юниты и замки его не останавливали.
            if (Physics.Raycast(ray, out hit)) {
                GetComponent<UnityEngine.AI.NavMeshAgent>().destination = hit.point;
            }
        }
	}

    bool isSelected() {
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in children) {
            if (r.gameObject.name == "SelectionCircle" && r.enabled) {                
                return true;
            }
        }
        return false;
    }
}