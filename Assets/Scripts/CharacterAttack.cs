using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterAttack : MonoBehaviour
{
    Animator anim;
    CharacterAiming CA;
    CharacterLocomotion CL;
    public Blink blink;
    public Color goldLinkColor;
    public float goldLinkIntensity;
    public float goldLinkDuration;
    public bool canGoldLink;
    public bool missedGoldLink;
    public Weapon currentWeapon;
    public Material weaponMat;
    public Color glowColor;
    public float glowIntensity;
    public float offset;
    public Transform target;
    public bool isAttacking;

    public float anticipationSpeed;
    public float recoverySpeed;
    public float attackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        CA = GetComponent<CharacterAiming>();
        weaponMat.EnableKeyword("_EMISSION");

        isAttacking = false;
        anim.speed = 1f;
        anim.ResetTrigger("gold_attack");
        anim.ResetTrigger("attack");
        canGoldLink = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isAttacking && !canGoldLink)
            {
                blink.BlinkME(goldLinkDuration * 2f, goldLinkIntensity, Color.red);
                missedGoldLink = true;
            }
            TeleportToTarget();
            if(!canGoldLink || missedGoldLink)            
                anim.SetTrigger("attack");
            else
                anim.SetTrigger("gold_attack");

            isAttacking = true;
            StartCoroutine(IsAttackingCheck());      
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("special");
            isAttacking = true;
            StartCoroutine(IsAttackingCheck());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("feint");

            isAttacking = false;
            anim.speed = 1f;
            anim.ResetTrigger("gold_attack");
            canGoldLink = false;
            missedGoldLink = false;
            GlowWeapon(0f, glowColor);
        }
    }

    public void TeleportToTarget()
    {
        //transform.position = ((transform.position - target.position).normalized * offset) + target.position;
        //transform.LookAt(target);
        //target.LookAt(transform);
    }

    public IEnumerator IsAttackingCheck()
    {
        yield return new WaitForSeconds(0.25F); // for transition
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        isAttacking = false;
        anim.speed = 1f;
        anim.ResetTrigger("gold_attack");
        anim.ResetTrigger("attack");
        canGoldLink = false;
        missedGoldLink = false;
        GlowWeapon(0f, glowColor);
        anim.ResetTrigger("feint");
    }

    //helper
    public void GlowWeapon(float intensity, Color color)
    {
        weaponMat.SetColor("_EmissionColor", color * intensity);
    }

    //animation events
    public void Anticipation()
    {
        GlowWeapon(glowIntensity, glowColor);
        anim.speed = anticipationSpeed;
    }

    public void Anticipation_(float speed)
    {
        GlowWeapon(glowIntensity, glowColor);
        anim.speed = speed;
    }

    public void OpenColliders()
    {
        currentWeapon.hitBox.collider.enabled = true;
        GlowWeapon(0f, glowColor);
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
        if(!missedGoldLink)
            blink.BlinkME(goldLinkDuration, goldLinkIntensity, goldLinkColor);
        else
            blink.BlinkME(goldLinkDuration*2f, goldLinkIntensity, Color.red);
        canGoldLink = true;
        StartCoroutine(Recover());
        StartCoroutine(EndGoldLink());
    }

    public IEnumerator EndGoldLink()
    {
        yield return new WaitForSeconds(goldLinkDuration);
        canGoldLink = false;
    }

    public IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.2f);
        GlowWeapon(0f, glowColor);
        anim.speed = 1f;
    }
}
