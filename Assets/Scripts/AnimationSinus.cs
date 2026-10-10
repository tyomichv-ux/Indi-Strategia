using UnityEngine;
using System.Collections;

public class AnimationSinus : MonoBehaviour {

    float animRemaining = 0.0f;
    public float animSpeed = 20.0f;
    public float intensity = 1.0f;

    Vector3 startScale;

	void Start () {
	   startScale = transform.localScale;
	}
	

	void Update () {
        if (animRemaining > 0.0f) {
            animRemaining -= animSpeed * Time.deltaTime;
            
            float s = Mathf.Sin(animRemaining) * intensity;
            transform.localScale = startScale + new Vector3(s, s, s);
        }
	}

    public void toggle() {
        animRemaining = 2 * Mathf.PI;
    }

    public bool inProgress() {
        return animRemaining > 0.0f;
    }
}
