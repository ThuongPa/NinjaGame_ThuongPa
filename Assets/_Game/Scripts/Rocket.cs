using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float rocketSpeed = 15f;
    [SerializeField] private GameObject hitVFX;
   
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    private void OnInit()
    {
        rb.velocity = transform.right * rocketSpeed;
        Invoke(nameof(OnDespawn), 10f);
    }
    private void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(100f);
            Instantiate(hitVFX, transform.position, transform.rotation);

            Debug.Log(collision.GetComponent<Character>().Hp);
            OnDespawn();
        }
    }
}
