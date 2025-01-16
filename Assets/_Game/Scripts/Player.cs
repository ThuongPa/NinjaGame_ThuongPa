using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 350.0f;
    [SerializeField] private float slideForce = 350.0f;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Rocket rocketPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject rocket;


    private Quaternion newRotation;

    private bool isGrounded;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isThrow = false;
    private bool isDead = false;
    private bool isRocket = false;
    private bool isSlide = false;

    private int coin = 0;

    private float horizontal;
    private float vertical;

    private bool getAttackKey = false;
    private bool getThrowKey = false;
    private bool getJumpKey = false;
    private bool getRocketKey = false;
    private bool getWeaponKey = false;
    private bool getSlideKey = false;


   
    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
     
    }

    private void Update()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.R))
        {
            getWeaponKey = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            getRocketKey = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
          
            getAttackKey = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            getThrowKey = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            getJumpKey = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            getSlideKey = true;
        }
       
    }
    // Start is called before the first frame update

    void FixedUpdate()
    {
       
        if(isDead) return;
        isGrounded = CheckGrounded();
        if (isAttack) return;
        if (isGrounded)
        {
            if (getWeaponKey)
            {
                ChangeWeapon();
                getWeaponKey = false;
            }
            if (getRocketKey){
                if (isRocket)
                {
                    Shoot();
                    
                }
                getRocketKey = false;
            }
            //Jump with space
            if (getJumpKey && isGrounded)
            {
                Jump();
                getJumpKey = false;
            }

            if (getAttackKey)
            {
                Attack();
                getAttackKey = false;
            }
            if (getThrowKey)
            {
                Throw();
                getThrowKey = false;
            }
            if (getSlideKey)
            {
                Slide();
                getSlideKey = false;
            }
           

            if (Mathf.Abs(horizontal) > 0.1f) ChangeAnim("running");
        }

        //Change Anim if falling
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("jumpout");
            isJumping = false;
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            RotatePlayer();
            Run();
        }
        else
        {
            if (isGrounded && !isJumping)
            {
                ChangeAnim("idle");
                rb.velocity = Vector2.zero;
            }
        }
    }

    private Vector3 savePoint;

   
    public override void OnInit() {
        base.OnInit();
        isDead = false;
        isAttack = false;
        isJumping = false;
        isThrow = false;
        transform.position = savePoint;
        ChangeAnim("idle");
        DeActiveAttackArea();
        SavePoint();
    }
    public override void OnDeath()
    {
        isDead = true;
        base.OnDeath();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }
    public void Attack()
    {
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttackArea();
        Invoke(nameof(DeActiveAttackArea), 0.5f);

    }
    public void Slide()
    {
        ChangeAnim("slide");
        isSlide = true;
        rb.AddForce(Vector2.right * slideForce);
    }
    private void ResetAttack()
    {

        isAttack = false;
        ChangeAnim("idle");
    }

    public void Jump()
    {  
        ChangeAnim("jump");
        isJumping = true;
       
        rb.AddForce(Vector2.up * jumpForce);
    }
    public void Shoot()
    {
        Instantiate(rocketPrefab, throwPoint.position, transform.rotation);
    }
    public void ChangeWeapon()
    {
        isRocket = !isRocket;
        rocket.SetActive(isRocket);
    }
    public void Throw()
    {
        ChangeAnim("throw");
        isThrow = true;
        Instantiate(kunaiPrefab,throwPoint.position,throwPoint.rotation);
        Invoke(nameof(ResetThrow), 0.5f);
    }
    private void ResetThrow()
    {
        ChangeAnim("idle");    
        isThrow = false;
    }
   
    private void RotatePlayer()
    {
        //Rotate player if input is -1 
        newRotation = Quaternion.Euler(0, 90.0f - 90.0f * horizontal, 0);
        transform.rotation = newRotation;
    }
    private void Run()
    {
        if (isGrounded)
        { ChangeAnim("running"); }
        rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {

            coin++;
            UIManager.Instance.SetCoin(coin);
            PlayerPrefs.SetInt("coin", coin);
            Debug.Log(coin);
            Destroy(collision.gameObject);
        }
        if(collision.tag == "DeadZone")
        {
            ChangeAnim("dead");
            isDead = true;

            Invoke(nameof(OnInit), 1f);
        }
    }
    
    
    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
        Debug.Log(horizontal);
    }
}
