using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // public AudioManager sound;
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private float horizontal;
    float moveSpeedDefault;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpForceClick;
    [SerializeField] private float jumpTime;
    [SerializeField] private float slideSpeed;
    [SerializeField] private Vector2 wallJumpDir;
    [SerializeField] private int multiJump;
    private float jumpCounter;
    private float wallJumpCounter;
    private bool isJump;
    public int jumpCount;
    public bool jump;
    public bool isSlide;
    bool wallJump;
    bool hasDir;
    Vector2 dir;
    Vector3 wall;
    float jumpHold;
    float jumpPower;
    float slideTime;
    float jumpSecondCounter;
   
    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;
    public bool isDashing;
    private bool canDash = true;
    private Vector2 dashDir;
    public float dashDelay;
    

    [Header("Attack")]
    public GameObject slash;
    public GameObject slash2;
    public GameObject slashUp;
    private float attackTime;
    public float attackCooldown;
    private bool canAttack = true;
    private int stateAttack=1;
    
    [Header("Attack Throw")]
    public GameObject lightning;
    public GameObject hand;
    public float theSpeed;
    public float throwDelay = 1;
    public float throwCounter;
    public bool canThrow;
    public Vector3 defaultPos;
    public float throwReleaseTime = 0.6f;
    

    [Header("Other")]
    [SerializeField] float knockBackForce;
    private float knockBackCounter;
    public float knockBackLength = 0.15f;
    public float knockbackDir;
    public Transform playerCheck;
    public Transform wallCheck;
    public Transform incoming;
    public bool isGrounded;
    public bool isTouchWall;
    public Animator setAnim;
    private Animator setAnimWeapon;
    public LayerMask theGround;
    public LayerMask theWall;
    private Rigidbody2D playerRB;
    private Collider2D impact;
    public bool canMove = true;
    public bool isFaceRight = true;
    EnemyBehaviour enemy;
    float defaultGravity;
    public float airGravity = 3;
    public float times = 2;
    public int countTest = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();
        playerRB = GetComponent<Rigidbody2D>();
        setAnim = GetComponent<Animator>();  
        impact = GameObject.FindGameObjectWithTag("PlayerHitbox").GetComponent<Collider2D>();
        dashTime = dashDuration;
        attackTime = attackCooldown;
        jumpCount = multiJump +1;
        moveSpeedDefault = moveSpeed;
        defaultGravity = playerRB.gravityScale;
        
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        isFaceRight = transform.localScale.x > 0;
        isTouchWall = Physics2D.OverlapCircle(wallCheck.position, .1f, theGround);
        isGrounded = Physics2D.OverlapCircle(playerCheck.position, .1f, theGround);
        if(canMove)
        {
            if (knockBackCounter <= 0)
            {
                if(throwCounter>throwDelay){
                    moveSpeed = 0.15f;
                    playerRB.gravityScale = 0;
                    playerRB.velocity = new Vector2(playerRB.velocity.x,-0.2f);
                } else {
                    moveSpeed = moveSpeedDefault;
                    playerRB.gravityScale = defaultGravity;
                }
                playerRB.velocity = new Vector2(moveSpeed * MoveInput(), playerRB.velocity.y);
                setAnim.SetBool("isJumping", !isGrounded);
                Jump();
                if (Mathf.Abs(playerRB.velocity.x) > 0.2f && !isGrounded){
                    wall = wallCheck.position;
                } 
                if (!jump && Mathf.Abs(playerRB.velocity.x) < 0.2f) {
                    wall = transform.position;
                }
                if (PlayerInputSetting.instance.attackClick && !isDashing && !isSlide && canAttack)
                {
                    
                    throwCounter = 0;
                    Attack();
                }

                
                if(PlayerInputSetting.instance.attack && !PlayerInputSetting.instance.attackClick && !PlayerAttackRay.instance.comeback && !PlayerAttackRay.instance.isStuck && !canThrow){
                    
                    
                    throwCounter+= Time.deltaTime;
                       
                }
                if(!PlayerInputSetting.instance.attack && throwCounter < throwReleaseTime){
                    throwCounter = 0;
                    
                }
                if(throwCounter == 0 && !canThrow){
                    
                    lightning.gameObject.SetActive(false);
                    PlayerAttackRay.instance.coll.enabled = false;
                    hand.gameObject.SetActive(false);
                    setAnim.SetBool("Throwing",false);
                }
                if(lightning != null){

                
                    if(throwCounter > throwDelay){
                        // setAnim.SetTrigger("Throw");
                        setAnim.SetBool("Throwing",true);
                        hand.gameObject.SetActive(true);
                        if((!PlayerInputSetting.instance.attack && throwCounter > throwReleaseTime) || throwCounter > 3){   
                            setAnim.SetTrigger("ThrowTrue");
                            Animator handAnim = hand.GetComponent<Animator>();
                            handAnim.SetTrigger("ThrowTrue");
                            canThrow = true;                                              
                            throwCounter = 0;
                            setAnim.SetBool("Throwing",false);
                        }
                
                            
                    } 
                }
                
                

                if (PlayerInputSetting.instance.dashClick && canDash)
                {
                    dashDuration = dashTime;
                    isDashing = true;
                    FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerDash",GetComponent<Transform>().position);

                }

                if (isDashing)
                {
                    PlayerDash();
                }
                else
                {
                    setAnim.SetBool("Dash", false);

                }


                if (MoveInput() < 0)
                {
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                }
                else if (MoveInput() > 0)
                {
                    transform.localScale = new Vector2(1, transform.localScale.y);
                }

                if(playerRB.velocity.y < 0 && !isGrounded && !isSlide){
                    setAnim.SetBool("isFalling",true);
                }else{
                    setAnim.SetBool("isFalling",false);
                } 
                
                setAnim.SetFloat("YVelocity", playerRB.velocity.y);
                setAnim.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
                setAnim.SetBool("IsSlide",isSlide);
            }
            else if(knockBackCounter > 0 ){
                knockBackCounter -= Time.deltaTime;
            }  
        } 
        else {
            dashDuration = 0;
            playerRB.velocity = new Vector2(0,0);
            setAnim.SetFloat("Speed",Mathf.Abs(playerRB.velocity.x));
            setAnim.SetBool("Dash",false);
            if(isGrounded){
                setAnim.SetBool("Jump",false);
            }
        }  
          
    }
    private float MoveInput(){
        if(PlayerInputSetting.instance.moveDir.x > 0.2f){
            return 1;
        } else if(PlayerInputSetting.instance.moveDir.x < -0.2f){
            return -1;
        } else {
            return 0;
        }
    }

    private void Flip(){
        isFaceRight = !isFaceRight;
        
        transform.localScale = new Vector2(transform.localScale.x *-1, transform.localScale.y);
    }
    
    private void Jump()
    {
        if (isTouchWall && !isGrounded && !PlayerInputSetting.instance.jump){
            slideTime += Time.deltaTime;
            // if(slideTime > 0.1f){
            //     isSlide = true;
            // }
            isSlide = true;            
        }
        if (isGrounded || (!isGrounded && !isTouchWall)){
            slideTime = 0;
            isSlide = false;
            
        }

        if(!isGrounded && jumpCount == multiJump+1){
            jumpCount -= 1;
        }
        if (isSlide && PlayerInputSetting.instance.moveDir.x !=0){
            playerRB.velocity = new Vector2(playerRB.velocity.x,Mathf.Clamp(playerRB.velocity.y, -slideSpeed, float.MaxValue));
            
        }
        if (!jump && (isGrounded || isSlide)){
            jumpCount = multiJump +1;
        }
        
        if (PlayerInputSetting.instance.jumpClick)
        {   
            
            jumpCount -= 1;
            if (isGrounded || jumpCount > 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Jump",GetComponent<Transform>().position);
                jump = true; 
                jumpCounter = jumpTime;
                jumpSecondCounter = jumpTime/2;
                jumpHold = 0;
                setAnim.SetTrigger("isJump");
                setAnim.SetBool("isJumping", true);
            }
            
            if (isSlide){
                wallJump = true;
                wallJumpCounter = jumpTime + 0.1f;
            }
        }
        jumpSecondCounter -= Time.deltaTime;
        jumpHold += Time.deltaTime;

        if(jumpHold > 0.15 && jumpHold < 0.2 && jumpCount == 1){
            jumpCounter = jumpTime * 0.8f;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpPower );
        }

        if (PlayerInputSetting.instance.jump && (jump || wallJump))
        {
            if (jumpCounter > 0)
            {
                jumpCounter -= Time.deltaTime;
                
                
                
                // playerRB.gravityScale = Mathf.Lerp(airGravity,defaultGravity,times);
                // times += 1f * Time.deltaTime;
                jumpPower = jumpForce;
                if(jumpCount == 2){
                    playerRB.velocity = new Vector2(playerRB.velocity.x, jumpPower);
                }
                if(jumpCount == 1 && jumpHold > 0.2){
                    
                    playerRB.velocity = new Vector2(playerRB.velocity.x, jumpPower);
                    
                }
                
            }
            else
            {
                jump = false;
                
            }
            
            if(wallJumpCounter > 0){
                if (!hasDir){

                    if (transform.localScale.x > 0){
                        dir = new Vector2(-1f , transform.localScale.y);
                    } else if (transform.localScale.x < 0){
                        dir = new Vector2(1f , transform.localScale.y);
                    }
                    hasDir = true;

                }
                wallJumpCounter -= Time.deltaTime;
                
                transform.localScale = dir;
                if(wallJumpCounter < 0.1f){
                    playerRB.velocity = new Vector2(playerRB.velocity.x, wallJumpDir.y);
                } else {
                    playerRB.velocity = new Vector2(wallJumpDir.x * dir.x, wallJumpDir.y);
                }
                
            } else {
                wallJump = false;
                hasDir = false;
                playerRB.gravityScale = defaultGravity;
                
                
            }

        }
        if (!PlayerInputSetting.instance.jump)
        {
            times = 0;
            // jumpHold = 0;
            wallJumpCounter = 0;
            jumpCounter = 0;
            jump = false;
            wallJump = false;
            hasDir = false;
            
        }
        if(!isSlide && !jump){
            wallJump = false;
            wallJumpCounter = 0;
        }

        
    }

    void PlayerDash(){
        
        dashDuration -= Time.deltaTime;
        dashDir = new Vector2(transform.localScale.x, 0f);    
        if (dashDuration > 0){
            setAnim.SetBool("Dash",true);
            playerRB.velocity = dashDir * dashSpeed;
            impact.enabled = false;
            canDash = false;
            
        } else {
            impact.enabled = true;
            isDashing = false;    
            StartCoroutine(ResetDash());   
            
        }
        
    }

    public void KnockBack(){
        knockBackCounter = knockBackLength;
        playerRB.velocity = new Vector2(KnockbackDir() * knockBackForce, knockBackForce);
    }

    public void KnockBackBoss(){
        knockBackCounter = knockBackLength;
        playerRB.velocity = new Vector2(KnockbackDir() * (knockBackForce* 0.5f), 0);
    }
    public void KnockBackGround(){
        knockBackCounter = knockBackLength;
        playerRB.velocity = new Vector2(-transform.localScale.x * (knockBackForce * 0.1f), 0);
    }
    
    void Attack(){
        int art = stateAttack;
        
            
        attackCooldown=attackTime;
        canAttack = false;
        if (art == 1 && PlayerInputSetting.instance.moveDir.y <=0.7f) {
            setAnim.SetTrigger("Attack");
            slash.SetActive(true);
            stateAttack=2;
        } 
        else if (art == 2 && PlayerInputSetting.instance.moveDir.y<=0.7f){
            setAnim.SetTrigger("Attack2");
            slash2.SetActive(true);
            stateAttack=1;
            
        } 
        else if(PlayerInputSetting.instance.moveDir.y>0.7f){
            setAnim.SetTrigger("AttackUp");
            slashUp.SetActive(true);
        }
        
        // Invoke(nameof(SetAttack),attackTime);
        StartCoroutine(ResetAttack());
            
        
        
    }
    
    public Transform GetNearestEnemy(){
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        float nearestEnemy = Mathf.Infinity;
        Transform theEnemy = null;

        foreach(GameObject en in enemy){
            float current;
            current = Vector3.Distance(transform.position, en.transform.position);
            if(current < nearestEnemy){
                nearestEnemy = current;
                theEnemy = en.transform;
            }

        }
        return theEnemy;
    }
    
    float KnockbackDir(){
        if(GetNearestEnemy() != null){
            if (transform.position.x < GetNearestEnemy().transform.position.x){
                knockbackDir = -1;
            } else if (transform.position.x > GetNearestEnemy().transform.position.x) {
                knockbackDir = 1;
            }
        } else {
            knockbackDir = transform.localScale.x;
        }
        return knockbackDir;
    }

    void SetAttack(){
        canAttack = true;
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(wall, .1f);
       
    }

    private IEnumerator ResetAttack(){
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
    private IEnumerator ResetDash(){
        yield return new WaitForSeconds(dashDelay);
        canDash = true;
    }



}
