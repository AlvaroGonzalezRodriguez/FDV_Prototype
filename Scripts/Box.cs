using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    
    CircleCollider2D circle;
    private float downTime;
    bool touched = false;
    
    // Start is called before the first frame update
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(touched == true &&Time.time >= downTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            circle.enabled = true;
            other.gameObject.GetComponent<Enemy>().respondToDamage(100, other.gameObject.name);
            downTime = Time.time + 3.0f;
            touched = true;
        }
    }
}
