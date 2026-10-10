using UnityEngine;
using System.Collections;

public class MoveByNpc : MonoBehaviour {

	void Update () {
        float range = GetComponent<Attack>().range;

        string enemyTag = GetComponent<Attack>().enemyTag;
        GameObject[] units = GameObject.FindGameObjectsWithTag(enemyTag);
        

        foreach (GameObject g in units) {
            if (g != null) {
                if (Vector3.Distance(transform.position, g.transform.position) <= range) {
                    return;
                }
            }
        }

        if (GetComponent<UnityEngine.AI.NavMeshAgent>().hasPath) {
            return;
        }


        if (units.Length > 0) {
            int index = Random.Range(0, units.Length);
            GameObject u = units[index];
            
            if (u != null) {
                Vector3 pos = transform.position;
                Vector3 target = u.transform.position;
                
                Vector3 dir = target - pos;
                dir = dir.normalized;
                Vector3 dest = pos + dir * (Vector3.Distance(target, pos) - range);

				GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(dest);

            }
        }
	}
}
