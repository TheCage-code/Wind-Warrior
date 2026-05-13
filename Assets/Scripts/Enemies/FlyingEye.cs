using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class FlyingEye : EnemyBase
{
    
    [SerializeField] Transform[] wayPoints;
    [SerializeField] GameObject ball;
    [SerializeField] Transform attackPoint;


    float attackTimer = 1.3f;
    bool isAttackReady = true;

    SpriteRenderer sR;


    int targetIndex = 0;
    protected override void Start()
    {
        base.Start();
        

        sR = GetComponentInChildren<SpriteRenderer>();

        foreach (Transform p in wayPoints)
        {
            p.SetParent(null);
        }
    }
    private void Update()
    {
        
        if (isAttackReady)
        {
            StartCoroutine(WaitAttack());
        }
        MoveToPoints();
        Flip();
        if (Vector2.Distance(transform.position, wayPoints[targetIndex].position) < 0.01f)
        {
            SelectNewTarget();
        }     
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;


        anim.SetTrigger("TakeHit");


        if (currentHealth <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Die();
        }
    }
    IEnumerator WaitAttack()
    {
        isAttackReady = false; 

        Instantiate(ball, attackPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(attackTimer);

        isAttackReady = true; 

    }
    
   public void SelectNewTarget()
    {
        targetIndex = Random.Range (0, wayPoints.Length);
    }
    private void MoveToPoints()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[targetIndex].position, moveSpeed * Time.deltaTime);
    }
    void Flip()
    {
        if (wayPoints[targetIndex].position.x < transform.position.x)
        {
            sR.flipX = true;
        }
        else sR.flipX = false;
    }

}