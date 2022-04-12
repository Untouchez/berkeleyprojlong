using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    [HideInInspector]
    public Enemy me;
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;

    public abstract void StartFunction();
    public abstract State UpdateFunction();
    public float updateRate;

    public void Start()
    {
        me = transform.root.GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = me.GetComponent<NavMeshAgent>();
        anim = me.GetComponent<Animator>();
    }
}
