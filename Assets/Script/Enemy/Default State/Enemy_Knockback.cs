using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knockback : StateMachineBehaviour
{
    EnemyBehaviour enemy;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
      
    }
}
