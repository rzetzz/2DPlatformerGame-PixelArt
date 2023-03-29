using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRay : MonoBehaviour
{
    public static PlayerAttackRay instance;
    public Animator setLightning;
    public Animator setFlash;
    private PlayerController player;
    public GameObject lightningFlash;
    Vector3 enemyPos;
    bool hasPosition;
    public bool isStuck;
    public float turnSpeed = 1;
    Quaternion enemyRotation;
    public Transform defaultPos;
    public float resetTime = 3;
    public Collider2D coll;
    public bool comeback;
    public float comebackSpeed = 3;
    public ParticleSystem comebackParticle;
    public ParticleSystem throwParticle;
    public GameObject theLightning;
    float counter;
    float throwCount;
    bool flash;
    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
       if(player.throwCounter > player.throwDelay && !comeback){
           
           coll.enabled = true;
           theLightning.gameObject.SetActive(true);
           
           transform.rotation = Quaternion.Slerp(transform.rotation, getPosition(), (turnSpeed * 0.5f) * Time.deltaTime);
           transform.localScale = transform.parent.localScale;
           if(player.throwCounter > player.throwReleaseTime){
               setLightning.SetBool("Idle",true);
               if(!flash){
                   lightningFlash.gameObject.SetActive(true);
                   flash = true;
               }
           }
       }
       if(player.canThrow){ 

           
           counter+=Time.deltaTime;
           if(counter > 0.01f){
                throwParticle.Play();
                counter =0;
            }
           
           transform.SetParent(null);    
           throwCount += Time.deltaTime;
           if(isStuck){
               transform.position += Vector3.zero * Time.deltaTime;
           }else {
               transform.position += transform.right * player.theSpeed * Time.deltaTime;
                if(GetNearestEnemy() != null){
                    if(Vector3.Distance(transform.position, GetNearestEnemy().position) < 10f){
                        transform.rotation = Quaternion.Slerp(transform.rotation, enemyRotation, turnSpeed * Time.deltaTime);
                        if(!hasPosition){   
                            enemyRotation =  getEnemyPosition(GetNearestEnemy());
                            hasPosition = true;
                        }
                    }
                }
            }
            if(throwCount > resetTime){
               Reseting();
            }
            
            
       } 

       if(comeback){
           setLightning.SetBool("Idle",false);
           theLightning.SetActive(false);
           
           counter+=Time.deltaTime;
           if(counter > 0.01f){
                comebackParticle.Play();
                counter =0;
            }
           
           isStuck = false;
           coll.enabled = false;
           
           transform.position =  Vector3.MoveTowards(transform.position,defaultPos.position,comebackSpeed * Time.deltaTime);
           if(Vector3.Distance(transform.position,defaultPos.position)<0.1f){
              
               transform.SetParent(player.transform);          
               comeback = false; 
           }
           
       }
      
       
    }
    Quaternion getPosition(){
        
        float angle = Mathf.Atan2(PlayerInputSetting.instance.axis.y, PlayerInputSetting.instance.axis.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        return targetRot;
       
    }
    Quaternion getEnemyPosition(Transform dir){
        Vector3 direction = transform.position - dir.position;
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        return targetRot;       
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

    
    
    void Reseting(){
        flash = false;
        hasPosition = false;
        player.canThrow = false;
        comeback = true;
        throwCount = 0;
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ground") && player.canThrow){
            isStuck = true;
            coll.enabled = false;
        }
    }
}
