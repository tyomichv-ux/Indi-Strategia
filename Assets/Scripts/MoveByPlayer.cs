using UnityEngine;
using System.Collections;

public class MoveByPlayer : MonoBehaviour {
	void Update () {
        if (Input.GetMouseButtonDown(1) && isSelected()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
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