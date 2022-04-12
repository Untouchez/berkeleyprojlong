using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : State
{
    public float attackRange;
    public bool inCombat;
    public void FixedUpdate()
    {
        if (inCombat)
        {
            me.transform.LookAt(player);
            anim.SetFloat("vertical", agent.velocity.magnitude / agent.speed);
        }
    }

    public override void StartFunction()
    {
        inCombat = true;
        me.updateRate = updateRate;
    }

    public override State UpdateFunction()
    {
        float distance = Vector3.Distance(player.position, me.transform.position);
        agent.SetDestination(player.position);
        if (distance <= attackRange)
            anim.SetTrigger("attack");
        return this;
    }
}
