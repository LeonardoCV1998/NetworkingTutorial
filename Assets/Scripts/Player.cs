using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHolaCountChanged))]
    int _holaCount = 0; 

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

        if(isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Sending Hola to the Server!");
            Hola();
        }
    }

    public override void OnStartServer()
    {
        Debug.Log("Player has been spawned on the Server!");
    }

    [Command]
    void Hola()
    {
        Debug.Log("Received Hola from client!");
        _holaCount += 1;
        ReplyHola();
    }

    [TargetRpc]
    void ReplyHola()
    {
        Debug.Log("Receive Hola from Server!");
    }

    [ClientRpc]
    void TooHigh()
    {
        Debug.Log("Too High!");
    }

    void OnHolaCountChanged(int oldCount, int newCount)
    {
        Debug.Log($"We had {oldCount} holas, but now we have {newCount} holas!");
    }
}
