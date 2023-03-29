using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float time;
    PlayerController player;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetOff(){
        this.gameObject.SetActive(false);
    }

    void CollActive(){
        coll.enabled = true;
    }
    void CollDisabled(){
        coll.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Ground"){
            player.KnockBackGround();
        }
    }


}
