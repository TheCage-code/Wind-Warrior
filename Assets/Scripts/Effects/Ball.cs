using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] int damage;

    Animator anim;
    Rigidbody2D rb;
    private void Start()
    {
        Destroy(gameObject, 2f);
        anim = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
