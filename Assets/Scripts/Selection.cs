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
        
        if (Input.GetMouseButtonUp(0)) { 
            GameObject[] units = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject g in units) {
                if (g != null) {
					Camera cam = Camera.main;
					Vector3 p = cam.WorldToScreenPoint(g.transform.position);
                    Vector2 screenPos = new Vector2(p.x, Screen.height - p.y);

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
