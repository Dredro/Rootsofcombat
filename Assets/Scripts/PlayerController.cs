using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Rigidbody2D rbody2D;
    public float moveSpeed;
    public Vector2 move;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rbody2D= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        OnUpdate();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void OnUpdate()
    {
        if(playerInput!= null)
        {
            moveAction = playerInput.actions["move"];
        }

       move = moveAction.ReadValue<Vector2>();
       
    }
    void Move()
    {
        rbody2D.velocity= move * moveSpeed;
    }

}
