using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [Header("Enemy")]
    public Transform target;
    protected NavMeshAgent agent;
    protected bool isAttacking = false;
    protected bool isAgentActive = true;
    // Use this for initialization
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    protected void SeekToTarget()
    {
        if (target != null)
        {
            if (isAgentActive)
                {
                agent.Resume();
                agent.SetDestination(target.position);
            }
            else
            {
                agent.Stop();
            }

        }
    }
    protected bool IsAtTarget()
    {
        if (!agent.hasPath)
            return false;
        return agent.remainingDistance <= agent.stoppingDistance;
    }
    protected virtual void Update()
    {
        SeekToTarget();
        if (CheckDeath())
        {
            OnDeath();
        }
    }
    protected virtual void Attack() { }
    protected virtual void OnDeath() { }
    protected virtual void FixedUpdate()
    {
        if (IsAtTarget())
        {
            Attack();
        }
    }
}
