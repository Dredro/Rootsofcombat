using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteBody;
    // Input Actions
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction jumpAction;

    // Input Actions Value
    private Vector2 move;
    private float jump;

    // Physics Private
    private bool isGround;
    private Rigidbody2D rbody2D;
    private float jumpTimer;
    private Collider2D coll;

    // Animation
    private Animator animator;

    [Header("Ground Layer")]
    public LayerMask layerMask;
    [Header("Permable Layer")]
    public LayerMask permableLayerMask;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Jump")]
    public float jumpHeight;
    public float jumpDelay;
    
    private void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rbody2D= GetComponent<Rigidbody2D>();
        coll= GetComponent<Collider2D>();
    }

    private void Update()
    {
        OnUpdate();
        GroundCheck();
        Descent();
    }
    private void FixedUpdate()
    {
        Jump();
        Move();
    }
    private void OnUpdate()
    {
        if(playerInput!= null)
        { // Read actions from Input Manager
            moveAction = playerInput.actions["move"];
            fireAction = playerInput.actions["fire"];
            jumpAction = playerInput.actions["jump"];
        }
        // Set value for Input Actions values
      move = moveAction.ReadValue<Vector2>();
      jump = jumpAction.ReadValue<float>();
    }
   private void Move()
    {
        animator.SetFloat("speed", Mathf.Abs(move.x));
        rbody2D.velocity = new Vector2(move.x * moveSpeed, rbody2D.velocity.y);
        if (move.x > 0)
        {
            spriteBody.flipX = true;
            spriteRenderer.flipX= true;
        }
        if (move.x < 0)
        {
            spriteBody.flipX = false;
            spriteRenderer.flipX = false;
        }
        
        
    }
   private void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2), 0.01f, layerMask);
    }

    private void Jump()
    {
        if((isGround) && (jump == 1) && (Time.time > jumpTimer))
        {
           rbody2D.AddForce(Vector2.up * jumpHeight,ForceMode2D.Impulse);
           jumpTimer = Time.time + jumpDelay;
        }
    }
    private void Descent()
    {
        bool permableUp,permableDown;
        
        permableUp = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + transform.localScale.y * 0.32f), 0.15f, permableLayerMask);
        permableDown = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - transform.localScale.y * 0.32f), 0.2f, permableLayerMask);
        if ((permableDown && move.y < -0.9f)||permableUp)
        {
            coll.isTrigger= true;
        }
        else
        {
            coll.isTrigger = false;
        }
    }
    
}
