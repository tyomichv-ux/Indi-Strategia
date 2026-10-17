using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour {
    public int current = 4;

    public Color color = Color.red;
    TextMesh tm;// НОВОЕ: кэшируем TextMesh
    Renderer tmRenderer;// НОВОЕ: кэшируем Renderer

    void Awake()
    {
        tm = GetComponentInChildren<TextMesh>();
        tmRenderer = tm.GetComponent<Renderer>();

        tmRenderer.material.color = color;
    }

    void Update () {
        int dashes = Mathf.Max(0, current);
        tm.text = new string('-', dashes);
        
        tm.transform.forward = Camera.main.transform.forward;
    }

    void LateUpdate() {
        // умер?   
        if (current <= 0) {
            Unit unit = GetComponent<Unit>();
            if (unit != null)
            {
                unit.Die();
            }
            else
            {
                Destroy(gameObject);
            }    
        }
    }
}