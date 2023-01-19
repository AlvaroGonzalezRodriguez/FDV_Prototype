using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Enemy[] enemy;
    
    public void Generate(int numEnemy)
    {
        for(int i = 0; i < numEnemy; i++)
        {
            int selectedEnemy = Random.Range(0, enemy.Length);
            createEnemy(selectedEnemy);
        }
    }
    
    public Enemy createEnemy(int selectedEnemy)
    {
        return Instantiate(enemy[selectedEnemy], new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5), Random.Range(transform.position.y - 5, transform.position.y - 5), 0), Quaternion.identity);
    }
}
