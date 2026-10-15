using UnityEngine;
using System.Collections;

public class BuildMenu : MonoBehaviour {    
    public int width = 200;
    public int height = 35;

    public GameObject prefab;
    public int maxCastles = 3; //ограничение на замки игрока
    public float buildCooldown = 10f;//пауза между постройками

    GameObject instance;
    float lastBuildTime = -999f;//последняя постройка, 1 замок доступен сразу

    void Update() {

        if (instance != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.name == "Ground") {
                    instance.transform.position = hit.point;
                    

                }
            }
            

            if (Input.GetMouseButton(0)) {
                instance = null;
                lastBuildTime = Time.time;//фиксируем момент постройки
            }
        }
    }
    
    void OnGUI() {
        bool limitReached = CountPlayerCastles() >= maxCastles;
        bool onCooldown = (Time.time - lastBuildTime) < buildCooldown;

        if (limitReached || onCooldown) return;

        GUILayout.BeginArea(new Rect(Screen.width/2 - width/2,
                                     Screen.height - height,
                                     width,
                                     height), "", "box");
        

        GUI.enabled = (instance == null);
        if (GUILayout.Button("BUILD CASTLE")) {
            instance = (GameObject)GameObject.Instantiate(prefab);
        }
        GUILayout.EndArea();
    }
    int CountPlayerCastles()
    {
        int count = 0;
        foreach (var c in FindObjectsOfType<CastlePlayer>())
        {
            if (c.gameObject.tag == "Player") count++;
        }
        return count;
    }
}