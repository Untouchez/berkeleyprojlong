using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimmyCombat : State
{
    private float timeRemaining;
    public bool inCombat;
    public float stompRate;

    public float rotateSpeed;
    public override void StartFunction()
    {
        inCombat = true;
        me.updateRate = updateRate;
    }

    public void Update()
    {
        if(inCombat)
            anim.SetFloat("vertical", agent.velocity.magnitude / agent.speed);
    }

    public override State UpdateFunction()
    {
        RotateTowards();
        if (Time.time - timeRemaining > 1 / stompRate)
        {
            timeRemaining = Time.time;

            if (Vector3.Distance(transform.position, player.position) < 3f) {
                anim.SetTrigger("stomp");
                agent.isStopped = true;
                agent.SetDestination(transform.position);
            } else if (Vector3.Distance(transform.position, player.position) < 8f) {
                me.transform.LookAt(player);
                anim.SetTrigger("cleave");
                agent.isStopped = true;
                agent.SetDestination(transform.position);
            } else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                agent.SetDestination(player.position);
                agent.isStopped = false;
                anim.ResetTrigger("cleave");
                anim.ResetTrigger("stomp");
            }
        } else if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >=1f){
            agent.SetDestination(player.position);
            agent.isStopped = false;
            anim.ResetTrigger("cleave");
            anim.ResetTrigger("stomp");
        }
        return this;
    }

    public void RotateTowards()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = player.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = rotateSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(me.transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(me.transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        me.transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
