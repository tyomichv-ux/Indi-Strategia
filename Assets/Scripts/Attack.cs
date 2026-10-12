using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    public float range = 5.0f; //радиус атаки

    public float interval = 1.5f;//частота стрельбы в сек
    float intervalElapsed = 0.0f;//сколько прошло с последнего выстрела

    public string enemyTag = "";//тэг враг

    public GameObject arrow;//arrow prefab
    

    void Update () {
        intervalElapsed += Time.deltaTime;
        
        if (intervalElapsed >= interval) {//когда стрелять
            GameObject[] units = GameObject.FindGameObjectsWithTag(enemyTag);//ищем всех врагов
            GameObject closestEnemy = null;//кандидат для атаки
            float closestDistance = range;//максимально допустимая дистанция
            foreach (GameObject g in units) {//только ищем, не стреляем
                // ещё жив?
                if (g != null) {
                    float dist = Vector3.Distance(g.transform.position, transform.position);
                    if (dist <= closestDistance)
                    {
                        closestDistance = dist;
                        closestEnemy = g;
                    }
                }
            }

            if (closestEnemy != null)
            {
                transform.LookAt(closestEnemy.transform);

                GetComponent<AnimationSinus>().toggle();

                GameObject a = (GameObject)Instantiate(arrow, transform.position, Quaternion.identity);

                a.GetComponent<Arrow>().target = closestEnemy.transform;
            }

            intervalElapsed = 0.0f;
        }
	}
}
// Вторая ошибка: в момент выстрела юнит клюёт носом,
// иногда наклоняется к земле.
// Почему. LookAt поворачивает весь объект, и по вертикали тоже,
// если цель выше или ниже.
// Как исправить: перед поворотом взять позицию цели и подставить
// в неё свою высоту. Тогда объект повернётся только
// влево-вправо и не будет наклоняться к земле.