using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFWM01 : Bullet
{

    private float bulletSpeed;
    private float initializationTime;
    private float bulletDmg;
	private float lastTrue;

    private Rigidbody2D rb;
    private Vector3 direction;
    private Animator animator;
	

    void OnEnable() {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in otherObjects) {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bulletSpeed = 5.0f;
        initializationTime = Time.timeSinceLevelLoad;
        bulletDmg = 10.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = direction * bulletSpeed;
        if(Time.time > lastTrue + 5.0f)
        {
           this.gameObject.SetActive(false);
        }
        if(animator.GetBool("isDestroy"))
        {
            bulletSpeed = 0.0f;
            bulletDmg = 0.0f;
        }
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in otherObjects) {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
        }
		
		//if(rb.velocity.x <= 0.5f && rb.velocity.y == 0.5f)
		//{
		//	DestroyBullet();
		//}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            if(!other.gameObject.GetComponent<MainMovement>().getDodge())
                animator.SetBool("isDestroy", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ExtWall" || other.gameObject.tag == "Wall") {
            this.gameObject.SetActive(false);
        }
    }

    public void SetDirectionBullet(Vector3 directionBullet)
    {
        direction = directionBullet;
    }

    public void DestroyBullet()
    {
        //Destroy(gameObject);
        this.gameObject.SetActive(false);
    }

    public override float getBulletDmg()
    {
        return bulletDmg;
    }
	
	public void setTrue(float ltrue)
	{
		lastTrue = ltrue;
	}
	
	public void setBulletSpeed(float speed)
	{
		bulletSpeed = speed;
	}
}
