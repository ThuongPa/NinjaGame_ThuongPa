using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject attackArea;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPrefab;
    // Start is called before the first frame update
    private float hp;
    public float Hp => hp;
    public string currentAnim;
    public bool IsDead => hp <= 0;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100, transform);
    }
    public virtual void OnDespawn()
    {
       
    }
    public void ActiveAttackArea()
    {
        attackArea.SetActive(true);
        Debug.Log("attack");
    }
    public void DeActiveAttackArea()
    {
        attackArea.SetActive(false);
    }
    public void OnHit(float damage)
    {
        if (hp >= 0)
        {
            hp -= damage;
            healthBar.SetNewHp(hp);
            Instantiate(combatTextPrefab,transform.position + Vector3.up,Quaternion.identity).OnInit(damage);
        }


        
    if (hp<=0)
            {
                hp = 0;
                Debug.Log("dead");
                OnDeath();
            }
    }
    public virtual void OnDeath()
    {
        
        ChangeAnim("dead");
     
        Invoke(nameof(OnDespawn), 2f);
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
