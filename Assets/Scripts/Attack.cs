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
