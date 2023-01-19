using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMovement : MonoBehaviour
{
    private Animator animator;
    private MainProperties properties;
    private float speed;
    private bool canMove = true;
    private bool dodging = false;
    private float rechargeDodge = 2.0f;
    private float lastDodge;
    private float dodgeAccuracy = 0.01f;
    private Vector3 dodgeDirection = new Vector3();
    private Vector3 dodgeGoal = new Vector3();
    
    public delegate void attacking(bool attack);
    public event attacking blockAttacking;

    //Notificador
    private MainAttack attack;
    private MainReactToEnviroment enviroment;
	
	public delegate void Parallax(float x, float y);
    public event Parallax moveParallax;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        properties = GetComponent<MainProperties>();
        speed = properties.getbaseSpeed();
        attack = GetComponent<MainAttack>();
        attack.movement += BlockMovement;
        enviroment = GetComponent<MainReactToEnviroment>();
        enviroment.movement += BlockMovement;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(canMove && animator.GetBool("isDead") == false)
        {
            animator.SetBool("isKnockback", false);
            if(horizontal != 0){
                Vector3 childScale = gameObject.transform.GetChild(1).transform.localScale;
                if(horizontal == -1) {
                    this.transform.localScale = new Vector3(-5.0f,5.0f,1.0f);
                    if(childScale.x > 0) 
                        gameObject.transform.GetChild(1).transform.localScale = new Vector3(-childScale.x, childScale.y, childScale.z);
                } else {
                    this.transform.localScale = new Vector3(5.0f,5.0f,1.0f);
                    if(childScale.x < 0) 
                        gameObject.transform.GetChild(1).transform.localScale = new Vector3(-childScale.x, childScale.y, childScale.z);
                }
                //No uso fisicas asi que lo muevo con el Transate si no tendria que utilizar algo parecido a -> rigidBody2D.velocity = new Vector2(h, rigidBody2D.velocity.y);
                transform.Translate(Vector3.right * horizontal * Time.deltaTime * speed);
                animator.SetBool("isRunning", true);
            } /*else {
                rigidBody2D.velocity = new Vector2(0.0f,rigidBody2D.velocity.y);
            }*/
            
            
            if(vertical != 0){
                transform.Translate(Vector3.up * vertical * Time.deltaTime * speed);
                animator.SetBool("isRunning", true);
				if(moveParallax != null)
                {
                    moveParallax(0.0f,vertical);
                }
            } /*else {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x,0.0f);
            }*/
            
            if(Input.GetKey(KeyCode.LeftShift) && Time.time > rechargeDodge + lastDodge)
            {
                blockAttacking(false);
                canMove = false;
                animator.SetBool("isDodging", true);
                dodgeGoal = Input.mousePosition;
                dodgeGoal = Camera.main.ScreenToWorldPoint(dodgeGoal);
                dodgeGoal.z = 0.0f;
                dodging = true;
                lastDodge = Time.time;
            }
        }

        if(dodging && animator.GetBool("isKnockback") == false)
        {
            dodgeDirection = dodgeGoal-transform.position;
            //transform.position = Vector3.MoveTowards(transform.position, dodgeDirection.normalized, speed * 2 * Time.deltaTime);
            if(dodgeDirection.magnitude > dodgeAccuracy && Time.time < lastDodge + 0.3f)
            {
                transform.Translate(dodgeDirection.normalized * Time.deltaTime * speed * 3);
            }else{
                blockAttacking(true);
                animator.SetBool("isDodging", false);
                dodging = false;
                canMove = true;
            }
        }

        if(animator.GetBool("isKnockback") && animator.GetBool("isDead") == false)
        {
            Vector3 knockDirection = (-horizontal) * Vector3.right + (-vertical) * Vector3.up;
            transform.Translate(knockDirection * Time.deltaTime * speed * 3);
        }

        if(horizontal == 0 && vertical == 0) {
            animator.SetBool("isRunning", false);
        }

    }

    private void BlockMovement(bool move)
    {
        canMove = move;
    }

    //Called in the animator
    public void FinishKnockback()
    {
        animator.SetBool("isKnockback", false);
        canMove = true;
    }

    public bool getDodge()
    {
        return dodging;
    }

}
