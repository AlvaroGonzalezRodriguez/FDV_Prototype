using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHitboxAttack : MonoBehaviour
{

    //Atacar enemigos
    public delegate void enemies(float damage, string name);
    public event enemies damage;

    private float totalDamage;

    private MainProperties properties;

    // Start is called before the first frame update
    void Awake()
    {
        properties = transform.parent.gameObject.GetComponent<MainProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Enemy") 
        {
            totalDamage = properties.getbaseMeleeAttackDmg();
            collider.gameObject.GetComponent<Enemy>().respondToDamage(totalDamage, collider.gameObject.name);
        }
    }
}
