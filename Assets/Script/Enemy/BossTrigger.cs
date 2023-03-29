using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BossTrigger : MonoBehaviour
{   
    public CinemachineVirtualCamera follow;
    public CinemachineVirtualCamera bossCam;
    public Animator boss;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            boss.SetTrigger("Start");
            follow.Priority = 0;
            bossCam.Priority = 1;
            gameObject.SetActive(false);
        }
    }
}
