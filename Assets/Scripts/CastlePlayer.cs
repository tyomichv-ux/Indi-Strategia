using UnityEngine;
using System.Collections;

public class CastlePlayer : MonoBehaviour {       
    void OnMouseDown() {
        if (!GetComponent<AnimationSinus>().inProgress()) {            
            GetComponent<UnitSpawner>().spawn();
        }
    }
}