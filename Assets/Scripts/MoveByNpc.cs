using UnityEngine;
using System.Collections;

public class MoveByNpc : MonoBehaviour {

	void Update () {
        // Здесь несколько ошибок движения врага. NavMesh — сетка по земле,
        // по которой юниту разрешено ходить. NavMeshAgent — компонент, который
        // ведёт его по этой сетке.
        //
        // 1. Враг иногда стоит и никуда не идёт.
        //    Путь считается не в тот же миг, а чуть позже. Пока он считается,
        //    hasPath равен false, хотя цель уже задана. Код ниже думает
        //    «пути нет» и каждый кадр задаёт новую случайную цель.
        //    Предыдущий расчёт выбрасывается, готовый путь так и не приходит.
        //    Как исправить: пока путь ещё считается, новую цель не задавать.
        //    Новую точку выбирать только когда старого пути правда нет.
        //
        // 2. Игрок увёл солдата, а враг доходит до старой точки и стоит.
        //    Как только hasPath стал true, метод выходит и больше не смотрит,
        //    куда цель делась.
        //    Как исправить: время от времени заново указывать, куда идти,
        //    даже если маршрут уже построен. Иначе враг не увидит, что цель сдвинулась.
        //
        // 3. Враг добегает до солдата и пробегает сквозь него, вместо того
        //    чтобы встать и стрелять.
        //    Когда кто-то уже в радиусе атаки, метод делает return и не
        //    останавливает агента. Тот доигрывает старый маршрут.
        //    Как исправить: перед выходом остановить агента и забыть старый
        //    маршрут, чтобы он встал и стрелял, а не доигрывал путь.
        //
        // 4. Чем дольше бой, тем сильнее тормоза.
        //    Каждый кадр каждый враг заново ищет всех целей через
        //    FindGameObjectsWithTag. Замки при этом плодят юнитов без конца
        //    (см. CastleNpc).
        //    Как исправить: обновлять список целей реже, не каждый кадр,
        //    и ограничить число юнитов у замка.
        float range = GetComponent<Attack>().range;

        string enemyTag = GetComponent<Attack>().enemyTag;
        GameObject[] units = GameObject.FindGameObjectsWithTag(enemyTag);
        

        foreach (GameObject g in units) {
            if (g != null) {
                if (Vector3.Distance(transform.position, g.transform.position) <= range) {
                    return;
                }
            }
        }

        if (GetComponent<UnityEngine.AI.NavMeshAgent>().hasPath) {
            return;
        }


        if (units.Length > 0) {
            int index = Random.Range(0, units.Length);
            GameObject u = units[index];
            
            if (u != null) {
                Vector3 pos = transform.position;
                Vector3 target = u.transform.position;
                
                Vector3 dir = target - pos;
                dir = dir.normalized;
                Vector3 dest = pos + dir * (Vector3.Distance(target, pos) - range);

				GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(dest);

            }
        }
	}
}
