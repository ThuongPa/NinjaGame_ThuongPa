using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            //doi huong cua enemy den huong cua player 
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.StopMoving();
            enemy.Attack();
            Debug.Log("attack state");
        }
    }
    public void OnExcute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy enemy)
    {
      
    }

    // Start is called before the first frame update
   
}
