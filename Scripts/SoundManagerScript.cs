using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    
	public static AudioClip chargeAttack, enemyShot, deathEnemyTiny, deathEnemyBig;
	static AudioSource audioSrc;
	
	// Start is called before the first frame update
    void Start()
    {
		chargeAttack = Resources.Load<AudioClip>("Grenade1Short");
		enemyShot = Resources.Load<AudioClip>("Grenade3Short");
		deathEnemyTiny = Resources.Load<AudioClip>("Grenade6Short");
		deathEnemyBig = Resources.Load<AudioClip>("Grenade7Short");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public static void PlaySound(string clip)
	{
		switch(clip)
		{
			case "charge":
				audioSrc.PlayOneShot(chargeAttack);
				break;
			case "enemyShot":
				audioSrc.PlayOneShot(enemyShot);
				break;
			case "deathEnemyTiny":
				audioSrc.PlayOneShot(deathEnemyTiny);
				break;
			case "deathEnemyBig":
				audioSrc.PlayOneShot(deathEnemyBig);
				break;
		}
	}
}
