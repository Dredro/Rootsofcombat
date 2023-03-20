using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputManagerEntry;

[RequireComponent(typeof(Rigidbody2D))]
public class New_Player : MonoBehaviour
{
    #region PlayerStats
    public Color color;
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    #endregion
    #region InputActions&InputValues
    private InputAction moveAction;
    private InputAction fire1Action;
    private InputAction jumpAction;
    private InputAction rotateAction;
    private InputAction fire2Action;
    private InputAction action1;
    private InputAction action2;
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
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        UpdateInputValues();
        Move();
    }
/*    private void LateUpdate()
    {
        UpdateSkin();
    }

    #region SkinUpdate
    private SpriteRenderer spriteRenderer;
    //public Skins leg;
    public void UpdateSkin()
    {
        string spriteName = spriteRenderer.sprite.name;
        spriteName = spriteName.Replace("blue-walk_", "");
        int spriteNr = int.Parse(spriteName);
        //spriteRenderer.sprite = leg.sprites[spriteNr];

    }
    #endregion*/

    #region ReadInputs
    void ReadInputActions(PlayerInput playerInput)
    {
        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            fire1Action = playerInput.actions["Fire1"];
            jumpAction = playerInput.actions["Jump"];
            rotateAction = playerInput.actions["Look"];
            fire2Action = playerInput.actions["Fire2"];
        }
    }
    void UpdateInputValues()
    {
        moveInputValue = moveAction.ReadValue<Vector2>();
        jumpInputValue = jumpAction.ReadValue<float>();
        fireInputValue = fire1Action.ReadValue<float>();
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