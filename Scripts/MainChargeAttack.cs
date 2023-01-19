using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChargeAttack : MonoBehaviour
{

    private MainProperties properties;
    private float initializationTime;
    private float totalDamage;
    private Rigidbody2D rb;
    private Vector3 direction;
    private Animator animator;

    void OnEnable() {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in otherObjects) {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        properties = GameObject.Find("MainCharacter").GetComponent<MainProperties>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initializationTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad - initializationTime > 2.0f)
        {
            DestroyChargeBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            totalDamage = properties.getbaseChargeShootAttackDmg();
            other.gameObject.GetComponent<Enemy>().respondToDamage(totalDamage, other.gameObject.name);
        } else if(other.gameObject.tag == "Bullet")
            other.gameObject.GetComponent<BulletFWM01>().DestroyBullet();
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            totalDamage = properties.getbaseChargeShootStayAttackDmg();
            other.gameObject.GetComponent<Enemy>().respondToDamage(totalDamage, other.gameObject.name);
        }else if(other.gameObject.tag == "Bullet")
            other.gameObject.GetComponent<BulletFWM01>().DestroyBullet();
    }

    public void SetDirectionBullet(float directionBullet)
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, directionBullet);
        transform.Translate(new Vector3(9,0,0));
    }

    public void DestroyChargeBullet()
    {
        Destroy(gameObject);
    }

}
