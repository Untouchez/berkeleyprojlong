using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterAttack : MonoBehaviour
{
    Animator anim;
    CharacterAiming CA;
    CharacterLocomotion CL;
    public Weapon currentWeapon;
    public Material weaponMat;
    public Color glowColor;
    public float glowIntensity;
    public float offset;
    public Transform target;
    public bool isAttacking;
    public int currentCombo;

    public float anticipationSpeed;
    public float recoverySpeed;
    public float attackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        CA = GetComponent<CharacterAiming>();
        weaponMat.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TeleportToTarget();
            anim.SetTrigger("attack");
            isAttacking = true;
            currentCombo = (currentCombo + 1) % 3;
            StartCoroutine(IsAttackingCheck(currentCombo));
            
        }
    }

    public void TeleportToTarget()
    {
        //transform.position = ((transform.position - target.position).normalized * offset) + target.position;
        //transform.LookAt(target);
        //target.LookAt(transform);
    }

    public IEnumerator IsAttackingCheck(int counter)
    {
        yield return new WaitForSeconds(0.3F); // for transition
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        isAttacking = false;
        anim.speed = 1f;
    }

    //animation events
    public void Anticipation()
    {
        GlowWeapon(glowIntensity, glowColor);
        anim.speed = anticipationSpeed;
    }

    public void OpenColliders()
    {
        currentWeapon.hitBox.collider.enabled = true;
        GlowWeapon(0f, Color.blue);
        anim.speed = attackSpeed;
    }

    public void CloseColliders()
    {
        currentWeapon.hitBox.collider.enabled = false;
        anim.speed = attackSpeed;
    }

    public void Recovery()
    {
        anim.speed = recoverySpeed;
        StartCoroutine(Recover());
    }

    public IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.2f);
        GlowWeapon(0f, glowColor);
        anim.speed = 1f;
    }


    public void GlowWeapon(float intensity, Color color)
    {
        weaponMat.SetColor("_EmissionColor", color * intensity);
    }
}
