using UnityEngine;

public class BossSpellEffect : MonoBehaviour
{
    Animator anim;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        
        Destroy(gameObject, 1.2f);
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            PlayerHealth health = collision.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(10);
            }
        }
    }


}
