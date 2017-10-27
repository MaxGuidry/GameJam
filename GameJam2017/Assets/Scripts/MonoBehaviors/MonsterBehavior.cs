using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    public readonly int SPEED = Animator.StringToHash("speed");
    public readonly int ATTACK = Animator.StringToHash("attack");
    public readonly int VERTICAL = Animator.StringToHash("ver");
    public readonly int HORIZONTAL = Animator.StringToHash("hor");
    public float attackDistance = .5f;
    public Animator m_anim;

    public Transform target;
    // Use this for initialization
    void Start()
    {
        m_anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = agent.velocity;
        m_anim.SetFloat(SPEED, velocity.magnitude);
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(target.transform.position, transform.position) < attackDistance)
        {
            Attack();
        }
    }

    void Attack()
    {
        m_anim.SetTrigger(ATTACK);
    }
}
