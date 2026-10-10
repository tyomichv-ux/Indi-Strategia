using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public Transform target;

    public float speed = 5.0f;

	void Update () {
       if (target != null) {
            transform.LookAt(target);

            transform.position = Vector3.MoveTowards(transform.position,
                                                     target.position,
                                                     speed * Time.deltaTime);

            if (transform.position == target.position) {
                --target.GetComponent<Health>().current;

                Destroy(gameObject);
            }
        } else {
            Destroy(gameObject);
        }
    }
}
