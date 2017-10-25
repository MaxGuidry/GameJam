using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
// ReSharper disable InconsistentNaming

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour, IDamageable, IDamager
{
    public enum State
    {
        Idle = 0,
        Moving = 1,
        Attacking = 2
    }

    public StatSciptable m_EnemyStats;
    public GameObject m_ProjectilePrefab;

    private NavMeshAgent m_Agent;
    private State m_CurrentState;
    private bool m_HasTarget;
    private Transform m_Target;
    private const float m_AttackCooldown = 1;
    private float m_NextAttackTime;
    void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Player") == null)
            return;
        m_HasTarget = true;
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        m_EnemyStats.GetStat("EnemyHealth").Value = 100;
    }
    // Use this for initialization
    void Start()
    {
        m_EnemyStats._Alive = true;
        if (m_Target)
        {
            m_CurrentState = State.Moving;
            StartCoroutine(UpdatePath());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_EnemyStats.GetStat("EnemyHealth").Value <= 0)
            m_EnemyStats._Alive = false;
        if (!m_EnemyStats._Alive)
            Destroy(gameObject);
        if (!m_HasTarget)
            return;
        if (Time.time > m_NextAttackTime)
        {
            if (Vector3.Distance(transform.position, m_Target.position) < m_Agent.stoppingDistance)
            {
                m_NextAttackTime = Time.time + m_AttackCooldown;
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator UpdatePath()
    {
        const float refreshRate = .25f;
        while (m_HasTarget)
        {
            if (m_CurrentState == State.Moving)
            {
                if (m_EnemyStats._Alive)
                    m_Agent.SetDestination(m_Target.position);
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

    private IEnumerator Attack()
    {
        m_CurrentState = State.Attacking;
        m_Agent.enabled = false;
        const float attackSpeed = 3;
        float percent = 0;
        var appliedDamage = false;
        while (percent <= 1)
        {
            if (percent >= .5f && !appliedDamage)
            {
                appliedDamage = true;
                Throw();
            }
            percent += Time.deltaTime * attackSpeed;
            yield return null;
        }
        m_CurrentState = State.Moving;
        m_Agent.enabled = true;
    }

    public void TakeDamage(float damage)
    {
        m_EnemyStats.GetStat("EnemyHealth").Value -= damage;
    }

    public void DoDamage(IDamageable defender)
    {
        defender.TakeDamage(m_EnemyStats.GetStat("EnemyDamage").Value);
    }

    public void Throw()
    {
        var g = Instantiate(m_ProjectilePrefab);
        g.transform.position = this.transform.position;
        g.GetComponent<Projectile>().Direction = m_Target.position - this.transform.position;
    }
}
