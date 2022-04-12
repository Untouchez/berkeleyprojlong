using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Animator anim;
    public Collider weaponCollider;
    public ParticleSystem stompParticleEffect;
    public ParticleSystem chargeParticle;
    public ParticleSystem releaseParticle;
    public ParticleSystem cleaveParticle;

    [Space(10)]
    public float anticipationSpeed;
    public float attackSpeed;
    public float recoverySpeed;

    [Space(10)]
    public float chargeSpeed;

    //animation events
    public void Anticipation()
    {
        anim.speed = anticipationSpeed;
    }

    public void Anticipation_(float speed)
    {
        anim.speed = speed;
    }

    public void OpenColliders()
    {
        weaponCollider.enabled = true;
        anim.speed = attackSpeed;
    }

    public void CloseColliders()
    {
        weaponCollider.enabled = false;
        anim.speed = attackSpeed;
    }

    public void Recovery()
    {
        anim.speed = recoverySpeed;
        StartCoroutine(Recover());
    }

    public void Stomp()
    {
        stompParticleEffect.Play(true);
    }

    public IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.2f);
        anim.speed = 1f;
    }

    public void Charge()
    {
        anim.speed = 0;
        attackCoroutine = StartCoroutine(Release());
        chargeParticle.gameObject.SetActive(true);
    }

    Coroutine attackCoroutine;
    public IEnumerator Release()
    {
        yield return new WaitForSeconds(chargeSpeed);
        chargeParticle.gameObject.SetActive(false);
        anim.speed = 1;
        releaseParticle.Play(true);
    }

    public void Cleave()
    {
        cleaveParticle.Play(true);
    }

    public void FootR()
    {

    }

    public void FootL()
    {

    }
}
