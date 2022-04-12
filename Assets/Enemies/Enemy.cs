using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Health
{
    NavMeshAgent agent;
    public Blink blink;
    public State currentState;
    public float updateRate;
    private float timeRemaining;
    public float chargeSpeed;

    public bool isDead;
    int hitCounter;
    public void Start()
    {
        timeRemaining = updateRate;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Time.time - timeRemaining > 1 / updateRate && !isDead)
        {
            timeRemaining = Time.time;
            RunStateMachine();
        }
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.UpdateFunction();
        if (nextState != null)
        {
            if (currentState != nextState)
                nextState.StartFunction();
            SwitchToTheNextStateMachine(nextState);
        }
    }

    public void SwitchToTheNextStateMachine(State nextState)
    {
        currentState = nextState;
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        blink.BlinkME(0.2f, 2f, Color.red);
        if (currentHealth <= 0)
        {
            if(!isDead)
                anim.Play("die", 0, 0);
            isDead = true;
            agent.SetDestination(transform.position);
            agent.isStopped = true;
            return;
        }
        hitCounter++;
        if (hitCounter >= 3)
        {
            anim.SetTrigger("damage");
            hitCounter = 0;
        }
        anim.speed = 1;
    }
}
