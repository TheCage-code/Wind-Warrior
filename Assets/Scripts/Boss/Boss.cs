using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBase
{
    public static Boss Instance;

    public Slider healthSlider;
    
    private Transform player;
   
  
    private bool isLookingRight = false;
   
   


    private void Awake()
    {
        if(Instance == null) Instance =this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    

    private void Update()
    {
        LookPlayer();
        UpdateUI();


    }

 public void LookPlayer()
    {
        if (player.position.x > transform.position.x && !isLookingRight || player.position.x < transform.position.x && isLookingRight)
        {
            Flip();
        }
     
    }
    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        


        anim.SetTrigger("TakeHit");


        if (currentHealth <= 0)
        {
            Die();
            Boss.Instance.healthSlider.gameObject.SetActive(false);
        }
    }
    public void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    public void Flip()
    {
        isLookingRight = !isLookingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

   








}