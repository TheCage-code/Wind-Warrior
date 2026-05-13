using UnityEngine;

public class FireBall : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
   

    

    [SerializeField] int damage = 15;
    [SerializeField] float speed;
    

    void Start()
    {
           
        anim = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, 2f);


    }

    void Update()
    {
        rb.AddForce(new Vector2(rb.linearVelocity.x * speed, rb.linearVelocity.y));
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))

        {

            Destroy(gameObject);
        }
        }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
