using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemy.SetTarget(collision.GetComponent<Character>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemy.SetTarget(null);
           
        }
    }
}
