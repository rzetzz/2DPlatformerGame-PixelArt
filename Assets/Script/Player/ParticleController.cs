using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public PlayerController player;
    public Rigidbody2D playerRb;
    public ParticleSystem move;
    public ParticleSystem dash;
    public ParticleSystem jump;
    public ParticleSystem jumpDir;
    public ParticleSystem slide;
    bool fall;
    float counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (move!= null){

        
            if(Mathf.Abs(playerRb.velocity.x) > 1f && player.isGrounded){  
                if(counter > 0.1f){
                    move.Play();
                    counter =0;
                }
                
            } 
           
        }

        if (dash!= null){

            if(player.isDashing){
                
                dash.Play();
            }

        }
        if (slide!= null){

            if(player.isSlide){
                if(counter > 0.05f){
                    slide.Play();
                    counter =0;
                }
            }

        }
        if (jump!= null){

            if(Input.GetButtonDown("Jump") && player.isGrounded && !player.isTouchWall){
                jump.Play();
            }

        }

        if (jumpDir!= null){

            if(player.jump && player.jumpCount >= 2){
                if(counter > 0.05f){
                    jumpDir.Play();
                    counter =0;
                }
                
            }

        }


    
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            jump.Play();
            
        }
    }
}
