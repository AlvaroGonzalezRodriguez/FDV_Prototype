using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TWM01 : Enemy
{
    private MainHitboxAttack mainHitbox;

    private Animator animator;

    public GameObject fire;

    private float health = 300.0f;
    private float actualHealth;
    private float attackDmg = 30.0f;
    private float speed = 1.0f;
    private string type = "TWM01";
    private Healthbar healthbar;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    public override void Start()
    {
        GameObject[] main = GameObject.FindGameObjectsWithTag("Player");
        GameObject hitbox = main[0].transform.GetChild(0).gameObject;
        mainHitbox = hitbox.GetComponent<MainHitboxAttack>();
        mainHitbox.damage += respondToDamage;
        actualHealth = health;
        healthbar = gameObject.transform.GetChild(0).GetComponent<Healthbar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("isDead"))
        {
            attackDmg = 0;
        }
    }

    public override float getAttackDmg()
    {
        return attackDmg;
    }

    public override float getSpeed()
    {
        return speed;
    }

    public override string getType()
    {
        return type;
    }

    public GameObject getFire()
    {
        return fire;
    }

    public override void respondToDamage(float damage, string name)
    {
        if(this.name == name)
        {
            animator.SetBool("isDamaged", true);
            actualHealth -= damage;
            healthbar.UpdateHealthBar(health, actualHealth);
            if(actualHealth <= 0)
            {
                animator.SetBool("isDead", true);
            }
        }
    }

    public override void destroyObject()
    {
        SoundManagerScript.PlaySound("deathEnemyBig");
        GameObject.Find("MainCharacter").GetComponent<MainAttack>().CountEnemiesDefeated();
        GameObject.Find("MainCharacter").GetComponent<MainProperties>().addHealth(150.0f);
        GameObject.Find("MainCharacter").GetComponent<MoveFloor>().LessEnemy();
        Destroy(gameObject);
    }

    public void EndDamage()
    {
        animator.SetBool("isDamaged", false);
    }
}
