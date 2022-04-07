using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15;
    Camera mainCamera;
    CharacterLocomotion CL;
    public bool freeAim = false;

    // Start is called before the first frame update
    void Start()
    {
        CL = GetComponent<CharacterLocomotion>();
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!freeAim && CL.rawInput != Vector2.zero) {
            float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
        }
    }

    
}
