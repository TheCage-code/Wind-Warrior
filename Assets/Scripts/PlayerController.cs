using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Idle, Running, Jumping, Falling, Attacking, Dying,Rolling }
    public PlayerState currentState;

    [Header("Movement Settings")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float attackCooldown;
    [SerializeField] float force;
    [SerializeField] float rollForce;
    [SerializeField] bool canAttack = true;

    [Header("Bool Settings")]
    public bool canMove = true;
    private bool isGrounded;
    public bool isImmune = false;

    [Header("Grounded Settings")]
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer; 

    float comboTimer = 1f;
    int comboCount = 0;

    PlayerCombat playerCombat;
    Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private GameObject currentPlatform;

    void Start()
    {
        playerCombat = GetComponent<PlayerCombat>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentState = PlayerState.Idle;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
            
        {
            if (currentPlatform != null)
            {              
                StartCoroutine(DisableCollision());
            }
           
        }
        


        if (comboCount > 0)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > 1.0f) 
            {
                comboCount = 0;
                comboTimer = 0;
            }
        }




        switch (currentState)
        {
            case PlayerState.Idle:
                CheckForAttack();
                CheckForRoll();
                CheckForJump();
                if (Mathf.Abs(moveInput) > 0.1f) ChangeState(PlayerState.Running);
                else if (rb.linearVelocity.y > 0.4f) ChangeState(PlayerState.Jumping);
                else if (rb.linearVelocity.y < -0.4f) ChangeState(PlayerState.Falling);
                break;

            case PlayerState.Running:
                CheckForAttack();
                CheckForRoll();
                CheckForJump();
                if (Mathf.Abs(moveInput) < 0.1f) ChangeState(PlayerState.Idle);
                break;

            case PlayerState.Jumping:
                CheckForAttack();
                if (rb.linearVelocity.y < -0.2f) ChangeState(PlayerState.Falling);
                break;

            case PlayerState.Falling:
                CheckForAttack();
                // if (Mathf.Abs(rb.linearVelocity.y) < 0.01f) ChangeState(PlayerState.Idle);
                if (isGrounded)
                {
                    ChangeState(PlayerState.Idle);
                }
                break;

            case PlayerState.Attacking:
                break;

            case PlayerState.Rolling:
                
                break;
        }

        Flip();
    }

    void FixedUpdate()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (currentState == PlayerState.Attacking) return;
        CheckForRun();


    }


    public void CheckForRun()
    {
        if (currentState == PlayerState.Rolling || currentState == PlayerState.Attacking) return;

        if (canMove)
        {
            //currentState = PlayerState.Running;
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    private void CheckForJump()
    {

        if (Input.GetKey(KeyCode.S)) return;
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            ChangeState(PlayerState.Jumping);
           // isGrounded = false;
        }
    }

    private void CheckForAttack()
    {
        if (Input.GetButtonDown("Fire1") && canAttack && isGrounded)
        {
            comboCount++;
            comboTimer = 0;
            canMove = false;
            SoundManager.instance.PlaySFX(SoundManager.instance.swordSwing);
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            if (comboCount > 2) comboCount = 1;           
            ChangeState(PlayerState.Attacking);          
            rb.linearVelocity = new Vector2(transform.localScale.x * force, rb.linearVelocity.y);
            StartCoroutine(AttackRoutine());
        }
        else if(Input.GetButtonDown("Fire1") && canAttack && !isGrounded)
        {
            comboCount++;
            if (comboCount > 2) comboCount = 1;        
            ChangeState(PlayerState.Attacking);
            StartCoroutine(AttackRoutine());
        }
    }

    private void CheckForRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canMove && isGrounded)
        {
            StartCoroutine(RollRoutine());
        }
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState == newState || currentState == PlayerState.Dying) return;
        currentState = newState;

        if (newState == PlayerState.Attacking) anim.SetInteger("attacks",comboCount);
        if (newState == PlayerState.Jumping) anim.SetTrigger("Jump");
        if (newState== PlayerState.Rolling) anim.SetTrigger("Roll");
        anim.SetBool("Run", newState == PlayerState.Running);
        anim.SetBool("isDown", newState == PlayerState.Falling);
    }

    private void Flip()
    {
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    IEnumerator AttackRoutine()
    {
        canAttack = false;

        
        yield return new WaitForSeconds(0.15f);

        
        if (currentState == PlayerState.Attacking) ChangeState(PlayerState.Idle);
        anim.SetInteger("attacks", 0);
        playerCombat.Attack();

        yield return new WaitForSeconds(attackCooldown - 0.2f);

        canAttack = true;
        canMove = true;
    }
    IEnumerator RollRoutine()
    {
        canMove = false;
        canAttack = false; 
        ChangeState(PlayerState.Rolling);
        isImmune = true;


        float direction = transform.localScale.x;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
        rb.AddForce(new Vector2(direction * rollForce, 0), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f); 

        canMove = true;
        canAttack = true;
        isImmune = false;
        ChangeState(PlayerState.Idle);
    }
    IEnumerator DisableCollision()
    {

        
        Collider2D[] playerColliders = GetComponents<Collider2D>();

        
        Collider2D platformCollider = currentPlatform.GetComponent<CompositeCollider2D>();
        

        if (platformCollider != null)
        {
            
            foreach (var pCol in playerColliders)
            {
                Physics2D.IgnoreCollision(pCol, platformCollider, true);
            }

            
            yield return new WaitForSeconds(0.4f);

           
            foreach (var pCol in playerColliders)
            {
                Physics2D.IgnoreCollision(pCol, platformCollider, false);
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground")) isGrounded = true;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground")) isGrounded = false;
    //}
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "OneWayPlatforms")
        {
            currentPlatform = collision.gameObject;
        }
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "OneWayPlatforms")
        {
            currentPlatform = null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }


}