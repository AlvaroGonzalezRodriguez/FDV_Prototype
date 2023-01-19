using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainReactToEnviroment : MonoBehaviour
{
    private Animator animator;
    private MainProperties properties;

    //Bloquear movimientos
    public delegate void block(bool movement);
    public event block movement;

    private Collider2D[] childrenColliders;

    // Start is called before the first frame update
    void Start()
    {
        properties = GetComponent<MainProperties>();
        animator = GetComponent<Animator>();

        childrenColliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in childrenColliders)
        {
            // checking if it is our collider, then skip it, 
            if(col != GetComponent<Collider2D>())
            {
                // if it is not our collider then ignore collision between our collider and childs collider
                Physics2D.IgnoreCollision(col, GetComponent<Collider2D>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(!GetComponent<MainMovement>().getDodge())
        {
            if(collider.gameObject.tag == "Enemy") 
            {
                if(collider.gameObject.GetComponent<Enemy>().getAttackDmg() != 0)
                {
                    Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                    properties.setDamageHealth(enemy.getAttackDmg());
                    if(properties.getActualHealth() <= 0){
                        animator.SetBool("isDead", true);
                    } else {
                        animator.SetBool("isAttacking1", false);
                        animator.SetBool("isAttacking2", false);
                        animator.SetBool("isKnockback", true);
                    }
                    movement(false);
                }
            }
            if(collider.gameObject.tag == "Bullet")
            {
                Bullet bullet = collider.gameObject.GetComponent<Bullet>();
                if(bullet.getBulletDmg() >= 20.0f){
                    animator.SetBool("isAttacking1", false);
                    animator.SetBool("isAttacking2", false);
                    animator.SetBool("isKnockback", true);
                }
                properties.setDamageHealth(bullet.getBulletDmg());
                if(properties.getActualHealth() <= 0){
                    animator.SetBool("isDead", true);
                }
            }
        }
        if(collider.gameObject.tag == "Wall" || collider.gameObject.tag == "ExtWall")
        {
            animator.SetBool("isKnockback", true);
            movement(false);
        }
    }
}
