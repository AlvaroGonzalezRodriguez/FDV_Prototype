using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTWM : Bullet
{
    private float initializationTime;
    private float fireDmg;
    private float counter = 0;

    private Animator animator;

    private bool upScale = false;
    
    void OnEnable() {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in otherObjects) {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        //bulletSpeed = 5.0f;
        initializationTime = Time.timeSinceLevelLoad;
        fireDmg = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad - initializationTime > 5.0f)
        {
            DestroyFire();
        }
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in otherObjects) {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
        }
        if(upScale)
        {
            counter += Time.deltaTime;
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(15.0f, 15.0f, 0.0f), counter / 5.0f);
        }
    }

    

    public void DestroyFire()
    {
        Destroy(gameObject);
    }

    public void SetScaleFire()
    {
        upScale = true;
    }

    public override float getBulletDmg()
    {
        return fireDmg;
    }
}
