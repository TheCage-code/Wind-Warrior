using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [Header("Hedef Nokta")]
    [SerializeField] private Transform targetPoint; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            collision.transform.position = targetPoint.position;
        }
    }
}
