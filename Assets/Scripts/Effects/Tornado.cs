using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField] float tornadoForce; 
    [SerializeField] float destroyTornado; 
    [SerializeField] float damage; 

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();

        
        float direction = transform.localScale.x;

        
        rb.linearVelocity = new Vector2(direction * tornadoForce, rb.linearVelocity.y);

        
        Destroy(gameObject, destroyTornado);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.GetComponent<EnemyBase>();

        if (enemy != null) 
        {
            enemy.TakeDamage(50);
        }
        //Slime slime = collision.GetComponent<Slime>();
        //if (slime != null)
        //{
        //    slime.TakeDamage(50);


        //}

    }
}