using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float damageInterval = 2f; 
    [SerializeField] private int damageAmount = 10;

    private float nextDamageTime; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if (Time.time >= nextDamageTime)
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);

                   
                    nextDamageTime = Time.time + damageInterval;

                    
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            nextDamageTime = 0;
        }
    }
}