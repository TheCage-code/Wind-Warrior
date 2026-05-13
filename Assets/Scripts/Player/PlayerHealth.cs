using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float maxMana;
    public float currentHealth;
    [HideInInspector] public float currentMana;

    bool isDead = false;

    public Slider healthSlider;
    public Slider manaSlider;
    PlayerController playerController;
    Animator anim;
    SpriteRenderer sR;

    void Start()
    {
        
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        sR = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        currentMana = maxMana;
        if (GameManager.instance != null)
        {
            GameManager.instance.SetPlayer(this);
        }
        UpdateUI();
    }

    private void Update()
    {
        OverHealthMana();
    }

    public void TakeDamage(int damage)
    {
        if (isDead || (playerController != null && playerController.isImmune))
        {
            return;
        }

        currentHealth -= damage;
        
        UpdateUI();

        if (currentHealth <= 0) 
        {
            anim.ResetTrigger("TakeHit");
            Die();
        }
        else
        {
            anim.SetTrigger("TakeHit");
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        anim.SetTrigger("Die");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        GetComponent<Collider2D>().enabled = false;
        Invoke("DisableSprite", 2.2f);
        Invoke("CallRestart", 3.0f);
    }
    void CallRestart()
    {
        GameManager.instance.RestartLevel();
    }
    public void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
        if (manaSlider != null)
        {
            manaSlider.value = currentMana / maxMana;
        }
    }

    void DisableSprite()
    {
        sR.enabled = false;
    }

    void OverHealthMana()
    {
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        if (currentMana > 100)
        {
            currentMana = 100;
        }
        if (currentMana < 0)
        {
            currentMana = 0;
        }
    }
}