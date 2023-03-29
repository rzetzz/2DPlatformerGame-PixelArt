using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Image[] life;
    public Sprite full,empty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < life.Length; i++){
            if ( i < PlayerStats.instance.maxHealth){
                life[i].enabled = true;
            } else {
                life[i].enabled = false;
            }

            if ( i > PlayerStats.instance.currentHealth-1){
                life[i].sprite = empty;
            } else if (i < PlayerStats.instance.currentHealth){
                life[i].sprite = full;
            }
        }
    }
}
