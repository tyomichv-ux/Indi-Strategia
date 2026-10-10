using UnityEngine;
using System.Collections;

public class BuildMenu : MonoBehaviour {    
    public int width = 200;
    public int height = 35;

    public GameObject prefab;
    
    GameObject instance;

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
            }
        }
    }
    
    void OnGUI() {
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
}