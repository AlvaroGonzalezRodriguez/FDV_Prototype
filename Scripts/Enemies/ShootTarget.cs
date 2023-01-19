using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTarget : StateMachineBehaviour
{
    private float shootCooldown = 1.5f;
    private float rafageCooldown = 3.0f;
    private float lastShoot;
    private float maxRange;

    private int maxShoots;
    private int currentShoot = 0;

    private GameObject player;

    private GameObject bullet;
	
	private GameObject[] bullets;
	private int index = 0;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		player = GameObject.Find("MainCharacter");
        //bullet = animator.GetComponent<FWM01>().getBullet();
		bullets = animator.GetComponent<FWM01>().getPool();
        maxRange = 10.0f;
        maxShoots = 10;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.time > lastShoot + shootCooldown)
        {

            var enemyPos = animator.GetComponent<Transform>().position;
            var heading = player.transform.position - enemyPos;
            var distance = heading.magnitude;
            var direction = heading / distance;
            
            if (heading.sqrMagnitude > maxRange * maxRange) {
                //currentShoot = 0;
                animator.SetBool("isAttacking", false);
            } else if(currentShoot != maxShoots){
                //GameObject bulletInstance = Instantiate(bullet, animator.GetComponent<Transform>().position, Quaternion.identity);
                bullets[index].SetActive(true);
				bullets[index].GetComponent<BulletFWM01>().setTrue(Time.time);
                SoundManagerScript.PlaySound("enemyShot");
				currentShoot++;
                Physics2D.IgnoreCollision(animator.GetComponent<Collider2D>(), bullets[index].GetComponent<Collider2D>());
				bullets[index].GetComponent<BulletFWM01>().setBulletSpeed(5.0f);
                bullets[index].GetComponent<BulletFWM01>().SetDirectionBullet(direction);
                lastShoot = Time.time;
				index++;
            } else if(Time.time > lastShoot + rafageCooldown){
                currentShoot = 0;
				index = 0;
            }
			
			if(index > animator.GetComponent<FWM01>().size)
			{
				currentShoot = 0;
				index = 0;
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
