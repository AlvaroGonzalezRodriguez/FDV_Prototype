using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : StateMachineBehaviour
{

    private GameObject player;
    private Transform thisObject;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("MainCharacter");
        thisObject = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float speed = animator.GetComponent<Enemy>().getSpeed();

        var enemyPos = animator.GetComponent<Transform>().position;
        var heading = player.transform.position - enemyPos;
        var distance = heading.magnitude;
        var direction = heading / distance;

        thisObject.Translate(direction * Time.deltaTime * speed);
        if(animator.GetComponent<Enemy>().getType() == "FWM01" && Vector3.Distance(player.transform.position, thisObject.position) < 10.0f)
        {
            animator.SetBool("isAttacking", true);
        } else if(animator.GetComponent<Enemy>().getType() == "TWM01" && Vector3.Distance(player.transform.position, thisObject.position) < 3.0f)
        {
            animator.SetBool("isAttacking", true);
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
