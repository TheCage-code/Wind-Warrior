using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FireWizard : EnemyBase
{
    [Header("Checking")]
    [SerializeField] float groundCheckDistance;
    [SerializeField] float wallCheckDistance;
    [SerializeField] float detectionRange;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject fireBall;
    [SerializeField] Slider healthSlider;

    [Header("Attack Settings")]
    [SerializeField] float fireRate = 1.5f;
    float nextFireTime;
    private Coroutine stunRoutine;

    bool isStunned = false;
  // bool isWaiting = false; 
    bool isObstacleWait = false; 
    bool canSeePlayer = false;

    float originalSpeed;

    protected override void Start()
    {
        originalSpeed = moveSpeed;
        base.Start();
    }

    void Update()
    {
        CheckPlayer();

        
        if (isStunned || isObstacleWait)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            if (anim != null) anim.SetBool("Run", false);
            return; 
        }

        
        if (canSeePlayer && Time.time >= nextFireTime && !isStunned)
        {
            Attack();
            nextFireTime = Time.time + fireRate;
        }

       
        if (!canSeePlayer)
        {
            base.Move();
            CheckObstacles();
        }
    }

    public void CheckPlayer()
    {
        Vector2 direction = transform.right * transform.localScale.x;
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, direction, detectionRange, playerLayer);

        if (hit.collider != null)
        {
            Debug.DrawRay(attackPoint.position, direction * hit.distance, Color.yellow); 
            canSeePlayer = true;
            //isWaiting = true;
        }
        else
        {
            Debug.DrawRay(attackPoint.position, direction * detectionRange, Color.red); 
            canSeePlayer = false;
           // isWaiting = false; 
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        GameObject ball =  Instantiate(fireBall, attackPoint.position, Quaternion.identity);
        ball.transform.right = transform.right * transform.localScale.x;
    }

    public void CheckObstacles()
    {
        bool isWall = Physics2D.Raycast(wallCheckPoint.position, transform.right * transform.localScale.x, wallCheckDistance, wallLayer);
        bool isGround = !Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, groundLayer);

        if ((isWall || isGround) && !isObstacleWait)
        {
            StartCoroutine(Wait());
        }
    }
    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateUI();


        if (stunRoutine != null)
        {
            StopCoroutine(stunRoutine);
        }

        
        stunRoutine = StartCoroutine(StunCoroutine());

        if (anim != null) anim.SetTrigger("TakeHit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    IEnumerator Wait()
    {
        isObstacleWait = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Flip();
        isObstacleWait = false;
    }

    IEnumerator StunCoroutine()
    {

        isStunned = true;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(1.5f); 

        isStunned = false;
        stunRoutine = null; 
    }
    public void UpdateUI()
    {
        healthSlider.value = currentHealth / maxHealth;
    }
}