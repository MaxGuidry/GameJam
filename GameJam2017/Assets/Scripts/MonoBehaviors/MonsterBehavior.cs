using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    
   // public float attackDistance = .5f;
    public Animator m_anim;

    public Transform target;
    // Use this for initialization
    void Start()
    {
        m_anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = agent.velocity;
        m_anim.SetFloat("Speed", velocity.magnitude);
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(target.transform.position, transform.position) < 5f)
        {
            Attack();
        }
    }

    void Attack()
    {
        m_anim.SetTrigger("Attack");
    }
}
