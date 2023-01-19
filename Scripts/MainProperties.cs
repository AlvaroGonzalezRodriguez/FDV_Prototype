using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProperties : MonoBehaviour
{
    private float baseHealth;
    private float additionalHealth;
    private float totalHealth;
    private float actualHealth;
    private float baseMeleeAttackDmg;
    private float baseSpeed;
    private float baseShootAttackDmg;
    private float baseChargeShootAttackDmg;
    private float baseChargeShootStayAttackDmg;

    private Healthbar healthbar;
    
    // Start is called before the first frame update
    void Awake()
    {
        baseHealth = 1000.0f;
        additionalHealth = 0.0f;
        totalHealth = baseHealth + additionalHealth;
        actualHealth = totalHealth;
        baseMeleeAttackDmg = 25.0f;
        baseSpeed = 6.0f;
        baseShootAttackDmg = 10.0f;
        baseChargeShootAttackDmg = 75.0f;
        baseChargeShootStayAttackDmg = 0.5f;
        healthbar = gameObject.transform.GetChild(1).GetComponent<Healthbar>();
        //healthbar.UpdateHealthBar(totalHealth, actualHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getbaseHealth()
    {
        return baseHealth;
    }

    public float getTotalHealth()
    {
        return totalHealth;
    }

    public float getActualHealth()
    {
        return actualHealth;
    }

    public void setDamageHealth(float change)
    {
        actualHealth -= change;
        healthbar.UpdateHealthBar(totalHealth, actualHealth);
    }

    public void addHealth(float health)
    {
        actualHealth += health;
        if(actualHealth > totalHealth){
            actualHealth = totalHealth;
        }
        healthbar.UpdateHealthBar(totalHealth, actualHealth);
    }

    public float getbaseMeleeAttackDmg()
    {
        return baseMeleeAttackDmg;
    }

    public float getbaseSpeed()
    {
        return baseSpeed;
    }

    public float getbaseShootAttackDmg()
    {
        return baseShootAttackDmg;
    }

    public float getbaseChargeShootAttackDmg()
    {
        return baseChargeShootAttackDmg;
    }

    public float getbaseChargeShootStayAttackDmg()
    {
        return baseChargeShootStayAttackDmg;
    }
    
}
