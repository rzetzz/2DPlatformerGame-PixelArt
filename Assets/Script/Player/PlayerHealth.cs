using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float immuneLength;
    float immuneCounter;
    public PlayerController player;
    SpriteRenderer playerSR;

    public float blinkTime;
    float blinkCounter;
    // Start is called before the first frame update
    void Start()
    {
        player= GetComponent<PlayerController>();
        playerSR = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (immuneCounter > 0){
            immuneCounter -= Time.deltaTime;
            if(blinkCounter >0){
                blinkCounter -= Time.deltaTime;
            }else if (blinkCounter<=0){
                blinkCounter = blinkTime;
            }
            if (blinkCounter > 0.1){
                playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, .5f);
            } else if (blinkCounter < 0.1 ){
                playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 1f);
                
            } 
        } else {
            playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 1f);
        }
    }

    public void GetDamage(){
        if (immuneCounter <= 0){
            player.setAnim.SetTrigger("GetHit");
            PlayerStats.instance.currentHealth --;
            player.KnockBack();
            immuneCounter = immuneLength;
            blinkCounter = blinkTime;
            // playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, .5f);
        }
    }
}
