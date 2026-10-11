using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {    

    Vector2 start = new Vector2(0, 0);
    Vector2 cur = new Vector2(0, 0);
    bool visible = false;
    
    Rect currentRect() {
        Vector2 min = new Vector2(Mathf.Min(start.x, cur.x),
                                  Mathf.Min(start.y, cur.y));
        Vector2 max = new Vector2(Mathf.Max(start.x, cur.x),
                                  Mathf.Max(start.y, cur.y));
        return new Rect(min.x, min.y, max.x-min.x, max.y-min.y);
    }
        
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            start.x = Input.mousePosition.x;
            start.y = Screen.height - Input.mousePosition.y;
            visible = true;
        }
    
        if (Input.GetMouseButton(0)) {
            cur.x = Input.mousePosition.x;
            cur.y = Screen.height - Input.mousePosition.y;
        }
        
        // Ошибка: клик по юниту его не выбирает, а снимает выделение со всех.
        // Клик по своему замку, чтобы создать солдата, тоже сбрасывает отряд.
        //
        // Почему. Выделение решается только рамкой. Если кнопку нажали и отпустили
        // почти на том же пикселе, ширина и высота рамки равны 0.
        // Contains у такой рамки не считает ни одну точку «внутри».
        // Всем юнитам выключается кружок под ногами.
        //
        // Как исправить. Если рамка меньше нескольких пикселей, считать это
        // кликом по одному юниту: пустить луч из камеры и включить кружок
        // только у того, в кого попали. Если луч ни в кого не попал — снять
        // выделение. Рамкой выделять только когда её реально протянули.

        //я это исправлю зуб даю.
        //обещаю
        if (Input.GetMouseButtonUp(0)) { 
            GameObject[] units = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject g in units) {
                if (g != null) {
					Camera cam = Camera.main;
					Vector3 p = cam.WorldToScreenPoint(g.transform.position);
                    Vector2 screenPos = new Vector2(p.x, Screen.height - p.y);
                    
                    // Забытая отладка. На каждый клик печатает имя каждого своего юнита
                    // в консоль Unity. На игру не влияет, только засоряет вывод.
                    // Как исправить: удалить эту строку.
                    Debug.Log("single selection break2" + g.name);

                    if (currentRect().Contains(screenPos)) {
                        setSelectionCircleVisiblity(g, true);
                    } else {
                        setSelectionCircleVisiblity(g, false);
                    }
                }
            }
            
            visible = false;
        }
	}

    void setSelectionCircleVisiblity(GameObject g, bool visible) {
        MeshRenderer[] children = g.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in children) {
            if (r.gameObject.name == "SelectionCircle") {                

                r.enabled = visible;
            }
        }
    }
        
    void OnGUI() {

        if (visible && !cur.Equals(start)) {
            GUI.Box(currentRect(), "");
        }
    }
}