using System.Collections;
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
    public FloatVariable refreshRate;
    public IntergerVariable m_AttackCooldown;
    public FloatVariable m_MaxEnemyHealth;

    private NavMeshAgent m_Agent;
    private State m_CurrentState;
    private bool m_HasTarget;
    private Transform m_Target;
    private float m_NextAttackTime;

    public void TakeDamage(float damage)
    {
        m_EnemyStats.GetStat("EnemyHealth").Value -= damage;
    }

    public void DoDamage(IDamageable defender)
    {
        defender.TakeDamage(m_EnemyStats.GetStat("EnemyDamage").Value);
    }

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Player") == null)
            return;
        m_HasTarget = true;
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        m_EnemyStats.GetStat("EnemyHealth").Value = m_MaxEnemyHealth.Value;
    }

    // Use this for initialization
    private void Start()
    {
        m_EnemyStats._Alive = true;
        if (!m_Target) return;
        m_CurrentState = State.Moving;
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_EnemyStats.GetStat("EnemyHealth").Value <= 0)
            m_EnemyStats._Alive = false;
        if (!m_EnemyStats._Alive)
            Destroy(gameObject);
        if (!m_HasTarget)
            m_CurrentState = State.Idle;
        if (!(Time.time > m_NextAttackTime)) return;
        if (!(Vector3.Distance(transform.position, m_Target.position) < m_Agent.stoppingDistance)) return;
        m_NextAttackTime = Time.time + m_AttackCooldown.Value;
        StartCoroutine(Attack());
    }

    private IEnumerator UpdatePath()
    {
        while (m_HasTarget)
        {
            if (m_CurrentState == State.Moving)
                if (m_EnemyStats._Alive)
                    m_Agent.SetDestination(m_Target.position);
            yield return new WaitForSeconds(refreshRate.Value);
        }
    }

    private IEnumerator Attack()
    {
        m_CurrentState = State.Attacking;
        const float attackSpeed = 3f;
        var percent = 0f;
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
    }

    public void Throw()
    {
        var g = Instantiate(m_ProjectilePrefab);
        g.transform.position = transform.position;
        g.GetComponent<Projectile>().Direction = m_Target.position - transform.position;
        g.GetComponent<Projectile>().Damage = m_EnemyStats.GetStat("EnemyDamage").Value;
    }
}