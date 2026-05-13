using UnityEngine;
using TMPro;


public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform shurikenAttackPoint;
    [SerializeField] float attackRadius;
    public float damage;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] GameObject tornado;
    [SerializeField] GameObject shuriken;
    [SerializeField] int maxShuriken = 5;
    [SerializeField] TextMeshProUGUI shurikenTxt;


    float shurikenRegenTimer = 0;
    float shurikenRegen = 2f;
    float currentShuriken;
    float nextFireTimer = 0f;
    public float shurikenFireRate = 0.05f;

    public AbilityData specialSkill,specialSkillE;
    private float nextReadyTime, nextReadyTimeE = 0f;
    public AbilitySlot qSlot,eSlot;
    PlayerController player;
    PlayerHealth playerHealth;


    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();    
        currentShuriken = maxShuriken;
        player = GetComponent<PlayerController>();

        
       
        shurikenRegenTimer = 0f; 
    }

    void Update()
    {
        if (currentShuriken < maxShuriken)
        {
            shurikenRegenTimer += Time.deltaTime;

            if (shurikenRegenTimer >= shurikenRegen)
            {
                currentShuriken++;
                shurikenRegenTimer = 0f;
            }
        }
        
        ShurikenUpdateUI();
        
        Qability();
        Eability();
        NinjaStar();

    }

   public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<EnemyBase>() != null)
            {
                enemy.GetComponent<EnemyBase>().TakeDamage(damage);
            }
        }

    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
    public void NinjaStar()
    {
        if (Input.GetMouseButtonDown(1) && Time.time  >= nextFireTimer && currentShuriken >= 1)
        {
            
            GameObject ninjaStar = Instantiate(shuriken, shurikenAttackPoint.position, Quaternion.identity);
            ninjaStar.transform.right = transform.right * transform.localScale.x;
            currentShuriken--;
            nextFireTimer = Time.time + shurikenFireRate;

        }
    }
    void ShurikenUpdateUI()
    {
        shurikenTxt.text = ((int)currentShuriken).ToString();
    }
    public void Qability()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= nextReadyTime && playerHealth.currentMana > 0)
        {
            player.canMove = false;
            specialSkill.Use(this.gameObject);
            playerHealth.currentMana -= 30;
            playerHealth.UpdateUI();
            

            Invoke("EnableMove", 2.1f); 

            nextReadyTime = Time.time + specialSkill.cooldown;
            qSlot.StartCooldownUI();
        }
    }

    public void Eability()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextReadyTimeE && playerHealth.currentMana > 0)
        {
            player.canMove = false;
            specialSkillE.Use(this.gameObject);
            playerHealth.currentMana -= 15;
            playerHealth.UpdateUI();


            Invoke("EnableMove", 0.5f);

           
            nextReadyTimeE = Time.time + specialSkillE.cooldown;
            eSlot.StartCooldownUI(); 
        }
    }

    private void EnableMove()
    {
        player.canMove = true;
    }

    private void UseTornado()
    {
        GameObject newTornado = Instantiate(tornado, attackPoint.transform.position, Quaternion.identity);
        newTornado.transform.localScale = transform.localScale;
       
    }

    
}
