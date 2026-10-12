using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public int current = 4;

    public Color color = Color.red;

    void Update () {
        TextMesh tm = GetComponentInChildren<TextMesh>();
        // Ошибка: в консоли красное ArgumentOutOfRangeException, когда юнита
        // добили две башни в один кадр.
        //
        // Почему. Жизни рисуются чёрточками. new string('-', current) значит
        // «строка из current знаков минус». У юнита осталась 1 жизнь.
        // Первая стрела делает current = 0, вторая в том же кадре делает -1.
        // Строку из минус одного символа C# создать не может и бросает ошибку.
        // Объект потом всё равно удалится в LateUpdate, но ошибка в консоли настоящая.
        //
        // Как исправить. Перед тем как собрать строку из чёрточек, не давать
        // числу жизней уйти ниже нуля.
        tm.text = new string('-', current);
        

        // Ошибка: у каждой полоски здоровья появляется своя копия материала,
        // лишние объекты копятся в памяти.
        //
        // Почему. Чтение .material первый раз говорит Unity: «сделай личную
        // копию материала для этого объекта». Полосок много — копий много.
        // Цвет и так один и тот же каждый кадр, переназначать его незачем.
        //
        // Как исправить. Покрасить один раз в Start и запомнить Renderer в поле,
        // а не искать его заново каждый кадр.
		tm.GetComponent<Renderer>().material.color = color;
        
        tm.transform.forward = Camera.main.transform.forward;
    }

    void LateUpdate() {
        // умер?   
        if (current <= 0) {
            Destroy(gameObject);
        }
    }
}
