using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGroup : MonoBehaviour
{
    public Transform CameraLook;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraLook.position = player.position;
    }
}
