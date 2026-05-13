using Unity.VisualScripting;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Ranges")]
    public float chaseRange = 7f;   
    public float attackRange = 2f;  
    public float attackCooldown = 2f; 
    private float nextAttackTime;

    [Header("References")]
    public Transform attackPoint;
    public float attackRadius = 0.5f;
    public LayerMask playerLayer;
    public int damageAmount = 1;
    public GameObject spellPrefab;

    [Header("Teleport Settings")]
    public float teleportDistance = 5f;

    public float yOffset = 0.5f;
    private int specialAttack;
    private int normalAttack;
    private float distanceToPlayer;
    bool alreadyTriggered = false;
    bool alreadyUsedTeleport = false;

    private Animator anim;
    private Transform player;
    private Boss boss;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = GetComponent<Boss>();
    }

    void Update()
    {
        if (player == null || boss == null) return;

        
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            
            anim.SetBool("Walk", false);

            if (Time.time >= nextAttackTime)
            {
                if (boss.currentHealth < 225 && !alreadyTriggered)
                {
                    SpellAttack();
                }
                else if (boss.currentHealth < 125 && !alreadyUsedTeleport)
                {
                   
                    Teleport();
                }
                else
                {
                    anim.SetTrigger("Attack");
                }
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else if (distanceToPlayer <= chaseRange)
        {
            
            anim.SetBool("Walk", true);
        }
        else
        {
            
            anim.SetBool("Walk", false);
        }
    }
    
    public void SpellAttack()
    {
       
            
        anim.SetTrigger("Cast");
        Vector3 spawnPosition = player.transform.position + new Vector3(0, yOffset, 0);
        Instantiate(spellPrefab, spawnPosition, Quaternion.identity);
        alreadyTriggered = true;


    }
    public void Teleport()
    {
        anim.SetTrigger("Teleport");
        alreadyUsedTeleport = true;
    }

    public void TeleportAction()
    {
        float direction = 0;

        
        if (player.position.x > transform.position.x)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }

        Vector3 newPos = transform.position + new Vector3(direction * teleportDistance, 0, 0);
        transform.position = newPos;
    }

    public void ExecuteAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        if (hit != null && hit.TryGetComponent(out PlayerHealth health))
        {
            health.TakeDamage(damageAmount);
        }
    }
}