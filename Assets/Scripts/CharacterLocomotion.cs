using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    CharacterAiming CA;
    Animator animator;
    Vector2 input;
    public Vector2 rawInput;

    [SerializeField] private float acceleration, decceleration;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CA = GetComponent<CharacterAiming>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }

    void HandleInputs()
    {
        rawInput.x = Input.GetAxisRaw("Horizontal");
        rawInput.y = Input.GetAxisRaw("Vertical");

        if (rawInput.x != 0)
            input.x = Mathf.MoveTowards(input.x, rawInput.x, acceleration * Time.deltaTime);
        else
            input.x = Mathf.MoveTowards(input.x, 0, decceleration * Time.deltaTime);

        if (rawInput.y != 0)
            input.y = Mathf.MoveTowards(input.y, rawInput.y, acceleration * Time.deltaTime);
        else
            input.y = Mathf.MoveTowards(input.y, 0, decceleration * Time.deltaTime);

        animator.SetBool("sprint", Input.GetKey(KeyCode.LeftShift));

        if(Input.GetKey(KeyCode.Space))
            animator.SetTrigger("jump");
        
    }
}
