using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Slime : EnemyBase
{
    [Header("Slime Move and Detections")]
    [SerializeField] float wallDetectionDistance = 0.5f;
    [SerializeField] float groundDetectionDistance = 1f;
    [SerializeField] int slimeDamage = 5;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] GameObject deathEffect;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Slider enemyHealthSlider;
    
    



    float originalSpeed;


    bool isWaiting = false;
    protected override void Start()
    {
        originalSpeed = moveSpeed;
        base.Start();
    }

    
    void Update()
    {
        if (isWaiting)
        {
            return;
        }
        Move();
        CheckObstacles();
    }
    public override void Move()
    {
        float facingDirection = transform.localScale.x;
        rb.linearVelocity = new Vector2(facingDirection * moveSpeed, rb.linearVelocity.y);
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        SoundManager.instance.PlaySFX(SoundManager.instance.slime);
        UpdateUI();
        StartCoroutine(StunCoroutine());

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    protected override void Die()
    {
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        
        

    }
    private void CheckObstacles()
    {
        bool isWall = Physics2D.Raycast(wallCheckPoint.position, transform.right * transform.localScale.x, wallDetectionDistance, wallLayer);

        bool isOnGround = Physics2D.Raycast(groundCheckPoint.position,Vector2.down,groundDetectionDistance, groundLayer);

        if (isWall || !isOnGround)
        {
            StartCoroutine(Wait());
            
        }

    }

    private void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    

    IEnumerator Wait()
    {
        isWaiting = true; 
        rb.linearVelocity = Vector2.zero; 

        yield return new WaitForSeconds(1f); 

        Flip(); 
        isWaiting = false; 
    }

    IEnumerator StunCoroutine()
    {
        moveSpeed = 0f;

        yield return new WaitForSeconds(0.7f);

        moveSpeed = originalSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(slimeDamage); 
            }
        }
    }

    public void UpdateUI()
    {
        enemyHealthSlider.value = currentHealth / maxHealth;
    }
}
