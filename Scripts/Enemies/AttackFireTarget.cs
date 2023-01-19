using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFireTarget : StateMachineBehaviour
{
    private float shootCooldown = 2.5f;
    //private float rafageCooldown = 3.0f;
    private float lastShoot;
    private float maxRange;

    private GameObject player;

    private GameObject fire;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("MainCharacter");
        fire = animator.GetComponent<TWM01>().getFire();
        maxRange = 10.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.time > lastShoot + shootCooldown)
        {
            var enemyPos = animator.GetComponent<Transform>().position;
            var heading = player.transform.position - enemyPos;
            
            if (heading.sqrMagnitude > maxRange * maxRange) {
                animator.SetBool("isAttacking", false);
            } else{
                GameObject fireInstance = Instantiate(fire, animator.GetComponent<Transform>().position, Quaternion.identity);
                Physics2D.IgnoreCollision(animator.GetComponent<Collider2D>(), fireInstance.GetComponent<Collider2D>()); 
                fireInstance.GetComponent<FireTWM>().SetScaleFire();
                lastShoot = Time.time;
            }
        } 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
