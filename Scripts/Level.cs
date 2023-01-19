using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI time;
    [SerializeField]
    private TextMeshProUGUI enemies;
    [SerializeField]
    private TextMeshProUGUI roundText;
    private int round = 1;
    private int nextRound = 25;
    [SerializeField]
    private GameObject player;
    private float timeBetweenSpawn = 7.0f;
    private float nextSpawn = 0.0f;
    private int numberEnemiesPerSpawn = 3;
    private int maxNumberOfEnemies = 15;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time.text = Time.time.ToString();
        enemies.text = "Enemies defeated: " + player.GetComponent<MainAttack>().GetEnemiesDefeated();
        roundText.text = "Round " + round;

        if(player.GetComponent<MainAttack>().GetEnemiesDefeated() >= nextRound)
        {
            round++;
            nextRound = nextRound * 2;
            if(timeBetweenSpawn > 1.0f)
                timeBetweenSpawn -= 0.5f;
            numberEnemiesPerSpawn++;
            maxNumberOfEnemies += maxNumberOfEnemies + 5;
        }

        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + timeBetweenSpawn;
            player.GetComponent<MoveFloor>().SpawnEnemies(numberEnemiesPerSpawn, maxNumberOfEnemies);
        }
		
    }
}
