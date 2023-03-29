using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float health;
    public bool canKnockBack = true;
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
    int count;
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
        
        if (knockbackCounter > 0){
            knockbackCounter -= Time.deltaTime;
        }
        
    
        if(health <= 0){
            theSr.enabled = false;
            coll.enabled = false;
            enemyRB.velocity = Vector2.zero;
            Destroy(gameObject,1f);
            
        }
        // Debug.Log(count);
    }

    private void FixedUpdate() {
        if (knockbackCounter > 0){
            // enemyRB.velocity = new Vector2(knockBackDir, enemyRB.velocity.y);
            enemyRB.velocity = -KnockBackDir() * knockbackForce;
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
        if (other.CompareTag("Attack")){
            FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyHit",GetComponent<Transform>().position);
            theSr.material = matFlash;
            get.Play();
            count ++;
            health -= PlayerStats.instance.damage;
            if(health <=0){
                deathParticle.Play();
                // setAnim.SetTrigger("Death");
            }
            Invoke(nameof(ResetMat), .1f);
            if(canKnockBack){
                isKnockback = true;
                knockbackCounter = knockbackTime;
            } else {
                player.KnockBackBoss();
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

    Vector3 KnockBackDir(){
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        return dir;
    }
    void ResetMat(){
        theSr.material = matDefault;
    }
}
