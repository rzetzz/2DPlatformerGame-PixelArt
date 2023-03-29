using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossState : MonoBehaviour
{

    public float jumpHeight, jumpDistance;
    public int totalJump = 3; 
    int jumpCount;
    public float speed;
    Rigidbody2D miniBossRb;
    Transform player;
    public Transform groundCheck;
    bool isGrounded;
    public LayerMask ground;
    Animator setAnim;
    bool avoid;
    float changeDir;
    bool stillJump;
    public float dashSpeed = 5;
    public GameObject charge;
    public GameObject throwSome;
    public float attackDistance = 1;
    int rand,num;
    public GameObject[] theThrow;
    // Start is called before the first frame update
    void Start()
    {
        miniBossRb = GetComponent<Rigidbody2D>();
        player = PlayerStats.instance.transform;
        setAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Vector3.Distance(transform.position, player.position));
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f,ground);
        
        if(Input.GetKeyDown(KeyCode.Backspace)){
            // setAnim.SetTrigger("JumpAttack");
            // stillJump = true;
            // jumpCount = 0;
            // setAnim.SetTrigger("DashAttack");
            // setAnim.SetTrigger("ChargeAttack");
            // setAnim.SetTrigger("ComboAttack");
            setAnim.SetTrigger("ThrowAttack");
        }
        setAnim.SetBool("StillJump", stillJump);
        

        
        
    }

    public void AvoidPlayer(){
        // if(Vector3.Distance(transform.position, player.position) < 1f){
        //     miniBossRb.velocity = new Vector2(speed * ChangeDir(), 0);
        // }
        miniBossRb.velocity = new Vector2(speed * ChangeDir(), 0);
    }

    public void ResetVelocity(){
        miniBossRb.velocity = Vector2.zero;
    }

    float ChangeDir(){
        if(player.transform.position.x > transform.position.x){
            changeDir = -1;
        } else {
            changeDir = 1;
        }
        return changeDir;
    }

    public void FlipTowardsPlayer(){
        if(transform.position.x < player.position.x){
            transform.localScale = new Vector3(-1,1,1);
        } else {
            transform.localScale = new Vector3(1,1,1);
        }
    }

    void JumpAttack(){

        float playerDir = player.position.x - transform.position.x;
        jumpCount++;
        if(jumpCount < totalJump){
            stillJump = true;
        }
        else if(jumpCount >=totalJump){
            stillJump = false;
            
        }
        miniBossRb.velocity = new Vector2(playerDir, jumpHeight);
    }
    void DashAttack(){
        miniBossRb.velocity = new Vector2(dashSpeed*-ChangeDir(), 0);
    }
    void ChargeAttack(){
        charge.SetActive(true);
    }
    void ComboAttack(){
        miniBossRb.velocity = new Vector2(attackDistance*-ChangeDir(), 0);
    }
    void Throwing(){
        foreach (GameObject obj in theThrow){
            obj.SetActive(true);
            obj.transform.SetParent(null);
        }
        // throwSome.SetActive(true);
        // throwSome.transform.SetParent(null);
    }

    void ResetParent(){
        foreach (GameObject obj in theThrow){
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            obj.transform.position = transform.position;
        }
    }
    

    public void RandomState(){

        
        switch (SetNumber())
        {
            case 1:
                setAnim.SetTrigger("JumpAttack");
                totalJump = Random.Range(1,4);
                stillJump = true;
                jumpCount = 0;
                break;
            case 2:
                setAnim.SetTrigger("DashAttack");
                break;
            case 3:
                setAnim.SetTrigger("ComboAttack");
                break;
            case 4:
                setAnim.SetTrigger("ChargeAttack");
                break;
            case 5:
                setAnim.SetTrigger("ThrowAttack");
                break;

        }

    }
    int SetNumber(){
        rand = Random.Range(1,6);
        
        while(rand == num){
            rand = rand = Random.Range(1,6);
            
        }
        num = rand;

       return rand;

    }

    
}
