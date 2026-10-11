using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    public float range = 5.0f;

    public float interval = 1.5f;
    float intervalElapsed = 0.0f;
    
    public string enemyTag = "";

    public GameObject arrow;
	
	void Update () {
        intervalElapsed += Time.deltaTime;
        
        if (intervalElapsed >= interval) {
            GameObject[] units = GameObject.FindGameObjectsWithTag(enemyTag);
            foreach (GameObject g in units) {
                // ещё жив?
                if (g != null) {
                    if (Vector3.Distance(g.transform.position, transform.position) <= range) {
                        // Ошибка: башня стреляет не в ближайшего врага, а в того,
                        // кто первым попался в списке и стоит в радиусе.
                        // Список каждый раз в другом порядке, поэтому цель прыгает
                        // с одного врага на другого.
                        // Как исправить: в цикле не стрелять сразу, а запомнить
                        // врага с самой маленькой дистанцией и выстрелить в него
                        // уже после цикла.
                        //
                        // Вторая ошибка: в момент выстрела юнит клюёт носом,
                        // иногда наклоняется к земле.
                        // Почему. LookAt поворачивает весь объект, и по вертикали тоже,
                        // если цель выше или ниже.
                        // Как исправить: перед поворотом взять позицию цели и подставить
                        // в неё свою высоту. Тогда объект повернётся только
                        // влево-вправо и не будет наклоняться к земле.
                        transform.LookAt(g.transform);
                     
                        GetComponent<AnimationSinus>().toggle();

                        GameObject a = (GameObject)Instantiate(arrow, 
                                                               transform.position, // default position
                                                               Quaternion.identity); // default rotation

                        a.GetComponent<Arrow>().target = g.transform;

                        break;
                    }
                }
            }

            intervalElapsed = 0.0f;
        }
	}
}
