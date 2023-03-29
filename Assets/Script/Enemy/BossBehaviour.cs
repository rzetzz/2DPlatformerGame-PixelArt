using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public float health;
    Animator setAnim;
    Material matDefault;
    public Material matFlash;
    SpriteRenderer theSr;
    public ParticleSystem get;
    public ParticleSystem deathParticle;
    Rigidbody2D enemyRB;
    PlayerController player;
    public float knockbackTime,knockbackForce;
    float knockbackCounter;
    float knockBackDir;
    public bool isKnockback;
    public bool isFaceRight;
    public float dir;
    float gravityDefault;
    Collider2D coll;
    bool afterDash;
    // Start is called before the first frame update
    void Start()
    {   
        setAnim = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        theSr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        coll = GetComponent<Collider2D>();
        matDefault = theSr.material;
        gravityDefault = enemyRB.gravityScale;
       
    }

    // Update is called once per frame
    void Update()
    {   
        // if(Mathf.Abs(transform.position.x - player.transform.position.x) < .5f){
    
        //     if (player.transform.position.x < this.transform.position.x){
        //         player.knockbackDir = -1;
        //     } else if (player.transform.position.x > this.transform.position.x) {
        //         player.knockbackDir = 1;
        //     }

        // }

        
        // if (knockbackCounter > 0){
        //     knockbackCounter -= Time.deltaTime;
        //     enemyRB.velocity = new Vector2(knockBackDir, enemyRB.velocity.y);
        //     enemyRB.gravityScale = 0;
            
        // } else {
        //     enemyRB.gravityScale = gravityDefault;
        //     enemyRB.velocity = Vector2.zero;
        //     isKnockback = false;
        // }
        if(health <= 0){
            theSr.enabled = false;
            coll.enabled = false;
            enemyRB.velocity = Vector2.zero;
            Destroy(gameObject,1f);
            
        }
    }

    private void FixedUpdate() {
        if (knockbackCounter > 0){
            knockbackCounter -= Time.fixedDeltaTime;
            enemyRB.velocity = new Vector2(knockBackDir, enemyRB.velocity.y);
            enemyRB.gravityScale = 0;
            afterDash = true;
            
        } else if(knockbackCounter<=0 && afterDash){
            enemyRB.gravityScale = gravityDefault;
            enemyRB.velocity = Vector2.zero;
            isKnockback = false;
            afterDash = false;
        }   
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Attack"){
            theSr.material = matFlash;
            get.Play();
            Debug.Log("HI");
            health -= PlayerStats.instance.damage;
            if(!player.isFaceRight){
                knockBackDir = -knockbackForce;
            } else {
                knockBackDir = knockbackForce;
            }
            isKnockback = true;
            if(health <=0){
                deathParticle.Play();
                setAnim.SetTrigger("Death");
            }
            Invoke(nameof(ResetMat), .1f);
            knockbackCounter = knockbackTime;
            if(setAnim != null){
                setAnim.SetTrigger("Knockback");
            }
            
        }
        if (other.tag == "Trap"){
            health = 0;
            if(health <=0){
                deathParticle.Play();
                setAnim.SetTrigger("Death");
            }
        }
    }
    public void Knockback(){
        
        enemyRB.velocity = new Vector2(knockBackDir, enemyRB.velocity.y);
    }
    void ResetMat(){
        theSr.material = matDefault;
    }
}
