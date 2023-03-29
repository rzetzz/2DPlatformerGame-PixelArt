using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera follow;
    public CinemachineVirtualCamera bossCam;
    public EnemyBehaviour bossHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth.health <=0){
            follow.Priority = 1;
            bossCam.Priority = 0;
        }
    }
}
