using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class New_Player : MonoBehaviour
{
    #region PlayerStats
    public EnumPlayerColor color;
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    #endregion
    #region InputActions&InputValues
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction jumpAction;
    private InputAction rotateAction;

    private Vector2 moveInputValue;
    private Vector2 rotateInputValue;
    private float jumpInputValue;
    private float fireInputValue;
    #endregion

    private PlayerInput playerInput;
    private Rigidbody2D rBody2D;
    private Animator animator;

    private void Start()
    {
        rBody2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        ReadInputActions(playerInput);
    }

    private void FixedUpdate()
    {
        UpdateInputValues();
        Move();
    }

    #region ReadInputs
    void ReadInputActions(PlayerInput playerInput)
    {
        if (playerInput != null)
        {
            moveAction = playerInput.actions["move"];
            fireAction = playerInput.actions["fire"];
            jumpAction = playerInput.actions["jump"];
            rotateAction = playerInput.actions["look"];
        }
    }
    void UpdateInputValues()
    {
        moveInputValue = moveAction.ReadValue<Vector2>();
        jumpInputValue = jumpAction.ReadValue<float>();
        fireInputValue = fireAction.ReadValue<float>();
        rotateInputValue = rotateAction.ReadValue<Vector2>();
    }
    #endregion

    #region Movement
    private void Move()
    { 
        animator.SetFloat("speed", Mathf.Abs(moveInputValue.x));
        rBody2D.velocity = new Vector2(moveInputValue.x * moveSpeed, rBody2D.velocity.y);
        if (moveInputValue.x > 0 || rotateInputValue.x > 0)
        {
            Quaternion quaternion = new Quaternion(0, 0, 0, 0);
            transform.localRotation = quaternion;
        }
        if (moveInputValue.x < 0 || rotateInputValue.x < 0)
        {
            Quaternion quaternion = new Quaternion(0, 180, 0, 0);
            transform.localRotation = quaternion;
        }
    }
    #endregion
}
