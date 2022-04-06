using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public Animator anim;
    public AnimatorOverrideController animatorOverrideController;

    public Weapon currentWeapon { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
    }
}
