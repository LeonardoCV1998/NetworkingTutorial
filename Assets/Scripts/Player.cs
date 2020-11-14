using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float inputHorizontal = Input.GetAxis("Horizontal");
            float inputVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(inputHorizontal * 0.1f, inputVertical * 0.1f, 0f);
            transform.position = transform.position + movement;
        }
    }

    private void Update()
    {
        HandleMovement();
    }
}
