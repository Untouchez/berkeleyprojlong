using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterAttack : MonoBehaviour
{
    Animator anim;
    CharacterAiming CA;
    CharacterLocomotion CL;
    public float offset;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        CA = GetComponent<CharacterAiming>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanMoveCoroutine != null)
                StopCoroutine(CanMoveCoroutine);
            CanMoveCoroutine = StartCoroutine(CanMove());

            CA.freeAim = true;
            transform.position = ((transform.position - target.position).normalized * offset) + target.position;
            transform.LookAt(target);
            target.LookAt(transform);
            anim.Play("attack");
            target.GetComponent<Animator>().Play("hit");
        }
    }
    Coroutine CanMoveCoroutine;
    IEnumerator CanMove()
    {
        yield return new WaitForSeconds(3.5f);
        CA.freeAim = false;
    }
}
