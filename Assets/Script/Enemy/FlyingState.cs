using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingState : MonoBehaviour
{
    public Transform[] locPoint;
    int currentPoint;
    EnemyBehaviour doing;
    private Transform player;
    public float chaseRange;
    public float attackRange;
    bool isChase;
    bool isAttack;
    Rigidbody2D enemyRB;
    public float chaseSpeed = 2;
    public float attackSpeed = 2;
    public float attackDelay;
    float attackCounter;
    public float attackDuration;
    float attacking;
    Vector3 dir;
    Vector3 comebackDir;
    Vector3 attackDir;
    float changeDir;
    float distance;
    public float waitTime;
    float waitCounter;
    bool comeBack;
    bool countUp;
    bool isStuck;
    public LayerMask theWall;


    
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerStats.instance.transform;
        enemyRB = GetComponent<Rigidbody2D>();
        doing = GetComponent<EnemyBehaviour>();

        foreach(Transform ispoint in locPoint){
            ispoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isStuck = Physics2D.OverlapBox(transform.position,Vector2.one,1/2 , theWall);
        // Debug.Log(distance);
        distance = Vector3.Distance(transform.position,player.position);
        dir = player.position - transform.position;
        dir.Normalize();
        
        if (waitCounter > 0){
            waitCounter -= Time.deltaTime;
        }
        if (attackCounter > 0){
            attackCounter -=Time.deltaTime;
        }

        if(attackCounter > (attackDelay * 0.1f)){
            
            attackDir = player.position - transform.position;
            attackDir.Normalize();
        
        }
        

        if (!doing.isKnockback){
            if(waitCounter <= 0){

                if(transform.position.x < player.position.x){
                    transform.localScale = new Vector3(-1,1,1);
                } else {
                    transform.localScale = new Vector3(1,1,1);
                }
                
                if(distance < chaseRange && !isAttack){
                    isChase = true;     
                } else if(distance > chaseRange && !isAttack){
                    isChase = false;
                }

                if(distance < attackRange && !isAttack){
                    attacking = attackDuration;
                    attackCounter = attackDelay;
                    enemyRB.velocity = Vector2.zero;
                    isChase =false;
                    isAttack = true;
                    
                }  

                if (attackCounter <= 0 && isAttack){
                    if (attacking > 0){
                        attacking -= Time.deltaTime;
                        enemyRB.velocity = attackDir * attackSpeed;
                        
                    } else {
                        comeBack = true;
                        waitCounter = waitTime;
                    }
                }
        
                if (isChase){
                    comeBack = false;
                    isAttack = false;
                    
                    if(isStuck){                      
                        enemyRB.velocity = new Vector2(chaseSpeed * -ChangeDir(), enemyRB.velocity.y);
                    } else {
                        enemyRB.velocity = dir * chaseSpeed;    
                    }
                             
                } else if(!isChase && !isAttack){
                    comeBack = true;
                }

            }
            if(comeBack){
                isAttack = false;
                Idle();
                if (enemyRB.velocity.x > 0){
                    transform.localScale = new Vector3(-1,1,1);
                } else {
                    transform.localScale = new Vector3(1,1,1);
                }

                
            }
        }
        else if (doing.isKnockback && attackCounter <= 0){
            attacking = 0;
        }
        
    }

    void Idle(){
        comebackDir = locPoint[currentPoint].position - transform.position;
        comebackDir.Normalize();
        
        if(Vector3.Distance(transform.position, locPoint[currentPoint].position ) > .2f){
            enemyRB.velocity = comebackDir * chaseSpeed;
        } else {
            enemyRB.velocity = Vector2.zero;
            if (currentPoint == locPoint.Length - 1){
                countUp = false;
            }
            if(currentPoint == 0){
                countUp = true;       
            }
            if(countUp){
                currentPoint++;
            } else {
                currentPoint--;
            }
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 10f);
        Gizmos.DrawWireSphere(transform.position, 3f);
        Gizmos.DrawWireCube(transform.position,new Vector2(0.4f ,1));
    }

    float ChangeDir(){
        if(enemyRB.velocity.x > 0){
            changeDir = -1;
        } else {
            changeDir = 1;
        }
        return changeDir;
    }
    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.tag == "Ground"){
    //          enemyRB.velocity = Vector2.zero;
    //          enemyRB.angularVelocity = 0;
    //          Debug.Log("hit");
    //     }
    // }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
             enemyRB.velocity = Vector2.zero;
             enemyRB.angularVelocity = 0;
             Debug.Log("hit");
        }
    }
}
