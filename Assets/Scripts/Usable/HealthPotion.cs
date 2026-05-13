using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] float giveHealth;
    
    

   
    



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().currentHealth += giveHealth;
            Destroy(gameObject);
            collision.GetComponent<PlayerHealth>().UpdateUI();
        }
       

       
    }

}
