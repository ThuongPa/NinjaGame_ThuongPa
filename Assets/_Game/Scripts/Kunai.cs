using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] private float kunaiSpeed = 10f;
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
        rb.velocity = transform.right * kunaiSpeed;
        Invoke(nameof(OnDespawn), 4f);
    }
    private void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(hitVFX,transform.position,transform.rotation);

            Debug.Log(collision.GetComponent<Character>().Hp);
            OnDespawn();
        }
    }
}
