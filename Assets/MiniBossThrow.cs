using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossThrow : MonoBehaviour
{
    Rigidbody2D theRb;
    public float distance = 5f;
    public float height =5f; 
    bool inGround;
    Vector3 defaultPos;
    public Transform defaultPosition;
    public GameObject miniBoss;
    SpriteRenderer theSR;
    private void Awake() {
        theRb = GetComponent<Rigidbody2D>();
        theSR = GetComponent<SpriteRenderer>();
        defaultPos = transform.position;
        // AttackThrow();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void AttackThrow(){
        
        theRb.velocity = new Vector2(distance * -miniBoss.transform.localScale.x ,height);
    }
    void ReesetVelocity(){
        
        theRb.velocity = Vector2.zero;
        theRb.angularVelocity = 0;
        // theRb.Sleep();
    }

    void None(){
       
    }
    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.tag == "Ground"){
    //         theRb.velocity = Vector2.zero;
    //     }
    // }
    // private void OnCollisionEnter2D(Collision2D other) {
    //     if(other.gameObject.tag == "Ground"){
            
    //         theRb.velocity = Vector2.zero;
    //     }
    // }
}
