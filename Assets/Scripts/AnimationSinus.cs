using UnityEngine;
using System.Collections;

public class AnimationSinus : MonoBehaviour {

    float animRemaining = 0.0f; //сколько времени анимации осталось
    public float animSpeed = 20.0f;//как быстро тикает таймер
    public float intensity = 1.0f;//насколько сильно пульсирует

    Vector3 startScale;

	void Start () {
	   startScale = transform.localScale;//запомнил изначаьлный размер объекта
	}
	

	void Update () {
        // Ошибка: после подпрыгивания замок или юнит остаётся сплющенным,
        // иногда вывернутым наизнанку. Так дёргаются замок при создании солдата
        // и юнит при каждом выстреле.
        //
        // Почему. Масштаб каждый кадр равен обычному размеру плюс синус.
        // Синус бывает отрицательным, поэтому объект сжимается и может
        // перевернуться. Когда таймер animRemaining доходит до нуля, этот if
        // перестаёт работать и масштаб остаётся каким был в последнем кадре.
        // Обратно к startScale (размер, запомненный в Start) его никто не ставит.
        //
        // Как исправить. Когда анимация кончилась, вернуть объект к размеру,
        // который запомнили в начале. Пока она идёт, прибавлять к масштабу
        // не сам синус, а его величину без знака, чтобы размер не становился
        // отрицательным.
        if (animRemaining > 0.0f) {
            animRemaining -= animSpeed * Time.deltaTime;
            
            float s = Mathf.Abs(Mathf.Sin(animRemaining)) * intensity;
            transform.localScale = startScale + new Vector3(s, s, s);
        }
        else
        {
            transform.localScale = startScale;
        }
	}

    public void toggle() {//запуск, ставлю 2п чтобы прошёл весь цикл
        animRemaining = 2 * Mathf.PI;
    }

    public bool inProgress() {
        return animRemaining > 0.0f;
    }
}
