using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Patrol : State
{
    public State combat;
    public Vector3[] destinations;
    public int curr;
    public bool inState = true;

    

    public override void StartFunction()
    {
        print("patrol");
        me.updateRate = updateRate;
    }

    public void Update()
    {
        if(inState)
            anim.SetFloat("vertical", agent.velocity.magnitude / agent.speed);
    }

    public override State UpdateFunction()
    {
        if(IsPlayerInfrontOfMe() || Vector3.Distance(transform.position,player.position) < 2f)
        {
            agent.SetDestination(player.position);
            inState = false;
            print("combat");
            return combat;
        }
        if (!agent.pathPending)
        {   
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath)
                {
                    curr++;
                    if (curr == destinations.Length)
                        curr = 0;
                    agent.SetDestination(destinations[curr]);
                }
            }
        }
        return this;
    }

    public bool IsPlayerInfrontOfMe()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = player.position - transform.position;

        if (Vector3.Dot(forward, toOther) > 0.25)
        {
            return true;
        }
        return false;
    }
}
