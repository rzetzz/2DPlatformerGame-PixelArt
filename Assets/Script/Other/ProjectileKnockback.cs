using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKnockback : MonoBehaviour
{
    float knockbackTime,knockbackForce;
    float knockbackCounter;
    bool deflect;
    Rigidbody2D theRb;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        theRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(deflect){
            theRb.velocity = -KnockBackDir() * 20;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Attack")){
            deflect = true;
            
        }

    }
    void setDeflect(){
        deflect = false;
    }
    Vector3 KnockBackDir(){
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        return dir;
    }
}
