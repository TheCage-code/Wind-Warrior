using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected float maxHealth;
    [HideInInspector]public float currentHealth;
    [SerializeField] protected float moveSpeed;


    protected Animator anim;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }


    public virtual void Move()
    {


        anim.SetBool("Run", true);


        float facingDirection = transform.localScale.x;
        rb.linearVelocity = new Vector2(facingDirection * moveSpeed, rb.linearVelocity.y);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
       
        
            anim.SetTrigger("TakeHit");
        
       
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    protected virtual void Die()
    {
        
        
            anim.SetTrigger("Die");
        
        
        Destroy(gameObject,1f);
    }
}
