using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isEnemy = true;

    public void Die()
    {
        Destroy(gameObject);
    }
}