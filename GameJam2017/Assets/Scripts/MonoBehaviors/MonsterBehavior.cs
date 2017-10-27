using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class MonsterBehavior : MonoBehaviour,IDamageable,IDamager
{
    private NavMeshAgent agent;
    
   // public float attackDistance = .5f;
    public Animator m_anim;

    public StatSciptable stats;
    private Transform target;

    private float backupDamage;
    // Use this for initialization
    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        stats.GetStat("EnemyHealth").Value = 100;
        stats.GetStat("EnemyDamage").Value = 10;
        backupDamage = stats.GetStat("EnemyDamage").Value;
        m_anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 6f;
        agent.angularSpeed = 0;
        StartCoroutine(TurnTo());
    }

    // Update is called once per frame
    void Update()
    {
        if (!target || !target.gameObject)
            return;
        var velocity = agent.velocity;
        m_anim.SetFloat("Speed", velocity.magnitude);
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(target.transform.position, transform.position) < 6f)
        {
            Attack();
        }
        
    }

    void Attack()
    {
        m_anim.SetTrigger("Attack");
        stats.GetStat("EnemyDamage").Value = backupDamage;
    }

    void NotAttacking()
    {

        stats.GetStat("EnemyDamage").Value = 0;
    }
    public void TakeDamage(float damage)
    {
        stats.GetStat("EnemyHealth").Value -= damage;
        if (stats.GetStat("EnemyHealth").Value <= 0)
        {
            stats.GetStat("EnemyHealth").Value = 0;
            stats._Alive = false;
            Die();
        }
    }

    void Die()
    {
        m_anim.SetTrigger("Dead");
        agent.isStopped = true;
        Destroy(this.gameObject,2);
    }
    public void DoDamage(IDamageable defender)
    {
        defender.TakeDamage(stats.GetStat("EnemyDamage").Value);
    }
    public IEnumerator TurnTo()
    {
        while (true)
        {
            if (!target)
                break;
            if (!target.gameObject)
                break;
            Quaternion q = this.transform.rotation;
            this.transform.LookAt(target.position + new Vector3(0,1,0));
            this.transform.rotation = Quaternion.Slerp(q, this.transform.rotation, .1f);
            yield return null;

        }
    }

    public void StartRot()
    {
        StartCoroutine(TurnTo());
    }
    public void StopRot()
    {
        StopCoroutine("TurnTo");
    }
}
