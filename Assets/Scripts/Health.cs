using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public Animator anim;
    public int maxHealth;
    public int currentHealth;

    int hitCounter;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = transform.root.GetComponent<Animator>();
    }

    public abstract void TakeDamage(int damage);

}
