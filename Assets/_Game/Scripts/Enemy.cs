using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    private IState currentState;
    private Quaternion newRotation;
    private bool isRight = true;
    private bool isDead = false;

    private Character target;
    public Character Target => target;
    private void Update()
    {
        if(currentState != null && isDead==false)
        {
            currentState.OnExcute(this);
        }
    }
   
    // Start is called before the first frame update
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }
    public override void OnDeath()
    {
        isDead = true;
        ChangeState(null);
        base.OnDeath();

    }
    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }
 
    public void ChangeState(IState newState)
    {
        if(currentState!=null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAttackArea();
        Invoke(nameof(DeActiveAttackArea), 0.5f);
    }
    public void Moving()
    {
        ChangeAnim("running");
        rb.velocity = transform.right * moveSpeed;
    }
    public bool IsTargetInRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) < attackRange)
            return true;
        else return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }
    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
     
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    public void SetTarget(Character character)
    {
        this.target = character;
        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else
        if (Target != null)
        {
            ChangeState(new PatrolState());
        }
        else ChangeState(new IdleState());
    }
}
