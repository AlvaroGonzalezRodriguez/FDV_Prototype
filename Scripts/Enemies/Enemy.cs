using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Start();
    public abstract float getAttackDmg();
    public abstract float getSpeed();
    public abstract string getType();
    public abstract void respondToDamage(float damage, string name);
    public abstract void destroyObject();
}
