using UnityEngine;
using UnityEngine.AI;

public class MoveByPlayer : MonoBehaviour {

    void Update () {
        if (Input.GetMouseButtonDown(1)) {

            bool sel = isSelected();
            Debug.Log("Выделен? " + sel);

            if (sel) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {

                    if (hit.transform.name == "Ground") {
                        GetComponent<NavMeshAgent>().destination = hit.point;
                    }
                } 
                else 
                {
                    Debug.Log("Луч ни во что не попал");
                }
            }
        }
    }

    bool isSelected() {
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in children)
        {
            if (r.gameObject.name == "SelectionCircle" && r.enabled) {
                return true;
            }
        }
        return false;
    }
}