using UnityEngine;

public class EnemyCanvas : MonoBehaviour
{
    public Transform enemyTransform;
    public Vector3 offset = new Vector3(0, 2, 0); 

    void LateUpdate()
    {
        if (enemyTransform != null)
        {
            transform.position = enemyTransform.position + offset;
        }
    }
}
