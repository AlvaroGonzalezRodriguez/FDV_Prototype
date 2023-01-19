using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWM01 : Enemy
{

    private MainHitboxAttack mainHitbox;

    private Animator animator;
    public GameObject bullet;

    private float health = 100.0f;
    private float actualHealth;
    private float attackDmg = 10.0f;
    private float speed = 3.0f;
    private string type = "FWM01";
    private Healthbar healthbar;
	
	private GameObject[] pool;
    public int size = 10;

    private void Awake()
    {
        animator = GetComponent<Animator>();
		
        pool = new GameObject[size];
        for(int i = 0; i < pool.Length; i++){
            pool[i] = Instantiate(bullet, this.transform.position, Quaternion.identity);
            pool[i].SetActive(false);
            //pool[i].GetComponent<Coin>().SetID(index);
        }
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
		
		for(int i = 0; i < pool.Length; i++){
			if(pool[i].activeSelf == false)
				pool[i].transform.position = this.transform.position;
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

    public GameObject getBullet()
    {
        return bullet;
    }
	
	public GameObject[] getPool()
	{
		return pool;
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
        SoundManagerScript.PlaySound("deathEnemyTiny");
        GameObject.Find("MainCharacter").GetComponent<MainAttack>().CountEnemiesDefeated();
        GameObject.Find("MainCharacter").GetComponent<MainProperties>().addHealth(50.0f);
        GameObject.Find("MainCharacter").GetComponent<MoveFloor>().LessEnemy();
        Destroy(gameObject);
    }

    public void EndDamage()
    {
        animator.SetBool("isDamaged", false);
    }
}
