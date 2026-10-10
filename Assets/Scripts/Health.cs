using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public int current = 4;

    public Color color = Color.red;

    void Update () {
        TextMesh tm = GetComponentInChildren<TextMesh>();
        tm.text = new string('-', current);
        

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
