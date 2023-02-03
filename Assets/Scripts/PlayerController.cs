using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public LayerMask layerMask;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction jumpAction;

    
    public bool isGround;

    private Rigidbody2D rbody2D;
    public float moveSpeed;
    public float jumpForce;
    private Vector2 move;
    private float jump;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rbody2D= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        OnUpdate();
        GroundCheck();
        Jump();
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
            fireAction = playerInput.actions["fire"];
            jumpAction = playerInput.actions["jump"];
        }

       move = moveAction.ReadValue<Vector2>();
      jump = jumpAction.ReadValue<float>();
    }
    void Move()
    {
        rbody2D.velocity += new Vector2(move.x * moveSpeed, 0f);
    }
    void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2), 0.1f, layerMask);
    }
    void Jump()
    {
        if(isGround && jump == 1)
        {
           rbody2D.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
        }
    }


}
