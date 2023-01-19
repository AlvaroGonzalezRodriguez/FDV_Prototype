using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAttack : MonoBehaviour
{
    private Animator animator;
    private bool canAttack = true;
    private int enemiesDefeated;
    private bool canChargeAttack = false;
    private float downTime, upTime, pressTime = 0.0f;
    private float countDown = 4.0f;
    private bool ready = false;

    public GameObject chargeBullet;

    //Bloquear movimientos
    public delegate void block(bool movement);
    public event block movement;

    private MainMovement mainMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mainMovement = GetComponent<MainMovement>();
        mainMovement.blockAttacking += BlockAttack;
        enemiesDefeated = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesDefeated%10==0)
        {
            canChargeAttack = true;
        }else{
            canChargeAttack = false;
        }

        if(canAttack)
        {
            //Basic 2 attack combo
            if(Input.GetMouseButtonDown(0))
            {
                movement(false);
                if(animator.GetBool("isAttacking1") == false){
                    ShakeCamera.Instance.ShakeCam(2.0f, 0.1f);
                    animator.SetBool("isAttacking1", true);
                } else {
                    ShakeCamera.Instance.ShakeCam(2.0f, 0.1f);
                    animator.SetBool("isAttacking2", true);
                }        
            }
            //Charge attack
            if(Input.GetKeyDown(KeyCode.Q) && canChargeAttack && ready == false)
            {
                movement(false);
                animator.SetBool("isChargingAttack", true);
                downTime = Time.time;
                pressTime = downTime + countDown;
                ready = true;
            }
            if (Input.GetKeyUp(KeyCode.Q)) {
                animator.SetBool("isChargingAttack", false);
                ready = false;
            }
            if (Time.time >= pressTime && ready == true) {
                animator.SetBool("isChargingAttack", false);
                animator.SetBool("isShootingChargeAttack", true);
                SoundManagerScript.PlaySound("charge");
                ShakeCamera.Instance.ShakeCam(5.0f, 1.0f);
                ready = false;

                Vector3 shootGoal = Input.mousePosition;
                shootGoal = Camera.main.ScreenToWorldPoint(shootGoal);
                shootGoal = shootGoal - transform.position;
                shootGoal.Normalize();
                float rot_z = Mathf.Atan2(shootGoal.y, shootGoal.x) * Mathf.Rad2Deg;

                GameObject bulletInstance = Instantiate(chargeBullet, transform.position, Quaternion.identity);
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), bulletInstance.GetComponent<Collider2D>()); 
                bulletInstance.GetComponent<MainChargeAttack>().SetDirectionBullet(rot_z);

            }
            //The player can move if he is not attacking
            if(animator.GetBool("isAttacking1") == false && animator.GetBool("isAttacking2") == false && animator.GetBool("isChargingAttack") == false && animator.GetBool("isKnockback") == false)
            {
                movement(true);
            }
        }
    }

    private void BlockAttack(bool attacking)
    {
        canAttack = attacking;
    }

    public void Attack1Ended()
    {
        animator.SetBool("isAttacking1", false);
    }

    public void Attack2Ended()
    {
        animator.SetBool("isAttacking2", false);
    }

    public void ChargeAttackEnded()
    {
        animator.SetBool("isShootingChargeAttack", false);
    }

    public GameObject getChargeAttack()
    {
        return chargeBullet;
    }

    public void CountEnemiesDefeated()
    {
        enemiesDefeated++;
    }

    public float GetEnemiesDefeated()
    {
        return enemiesDefeated;
    }
}
