using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Hitbox : MonoBehaviour
{
    public static CharacterAttack CA;
    public ParticleSystem dragEffect;
    public ParticleSystem bloodEffect;
    public Weapon weapon;
    public Collider collider;
    Animator anim;
    public float hitSpeed;
    public float hitDuration;

    public void Start()
    {
        anim = transform.root.GetComponent<Animator>();
        CA = anim.transform.GetComponent<CharacterAttack>();
        collider = GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        print("hit: " + other.transform);
        if (CA.isAttacking)
        {
            if (other.GetComponent<Health>())
            {
                other.GetComponent<Health>().TakeDamage(weapon.damage);
                StartCoroutine(EnteredSomething());
                bloodEffect.transform.position = other.ClosestPoint(transform.position);
                bloodEffect.Play(true);
            }
        }   

    }

    public void OnTriggerStay(Collider other)
    {
        if (CA.isAttacking)
        {
            if (!other.transform.root.GetComponent<Health>())
            { 
                dragEffect.transform.position = other.ClosestPoint(transform.position);
                dragEffect.Play(true);
            }
        }
    }

    public IEnumerator EnteredSomething()
    {
        anim.speed = hitSpeed;
        yield return new WaitForSeconds(hitDuration);
        anim.speed = 1f;
    }
}
