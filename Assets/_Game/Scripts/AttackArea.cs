using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Enemy")
        {
            Debug.Log("Hit");
            collision.GetComponent<Character>().OnHit(30f);
        }
    }
}
