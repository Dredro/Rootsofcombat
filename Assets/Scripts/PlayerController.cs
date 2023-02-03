using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{

    // Input Actions
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction jumpAction;

    // Input Actions Value
    private Vector2 move;
    private float jump;
    private float fire;

    // Physics Private
    private bool isGround;
    private Rigidbody2D rbody2D;
    private float jumpTimer;
    private Collider2D coll;

    [Header("Ground Layer")]
    public LayerMask layerMask;
    [Header("Permable Layer")]
    public LayerMask permableLayerMask;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Jump")]
    public float jumpHeight;
    public float jumpDelay;

    [Header("Current Weapon")]
    public GameObject objWeapon;
    public IWeapon curWeapon;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rbody2D = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        OnUpdate();
        GroundCheck();
        
        Descent();
        Fire();



        //DEBUG
        if (Input.GetKeyDown("backspace"))
        {
            ChangeWeapon(GameManager.Instance.weaponsList[0]);
            print("a");
        }
    }
    private void FixedUpdate()
    {
        Jump();
        Move();
    }
    private void OnUpdate()
    {
        if (playerInput != null)
        { // Read actions from Input Manager
            moveAction = playerInput.actions["move"];
            fireAction = playerInput.actions["fire"];
            jumpAction = playerInput.actions["jump"];
        }
        // Set value for Input Actions values
        move = moveAction.ReadValue<Vector2>();
        jump = jumpAction.ReadValue<float>();
        fire = fireAction.ReadValue<float>();

    }
    private void Move()
    {
        rbody2D.velocity += new Vector2(move.x * moveSpeed, 0f);
    }
    private void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2), 0.01f, layerMask);
    }

    private void Jump()
    {
        if ((isGround) && (jump == 1) && (Time.time > jumpTimer))
        {
            rbody2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            jumpTimer = Time.time + jumpDelay;
        }
    }
    private void Descent()
    {
        bool permableUp, permableDown;
        permableUp = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2), 0.01f, permableLayerMask);
        permableDown = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2), 0.01f, permableLayerMask);
        if ((permableDown && move.y < -0.9f) || permableUp)
        {
            coll.isTrigger = true;
        }
        else
        {
            coll.isTrigger = false;
        }
    }
    private void Fire()
    {
        if (fire == 1)
        {
            if (curWeapon != null)
                curWeapon.Fire1();
        }
        
    }
    public void ChangeWeapon(GameObject newWeapon)
    {
        if(curWeapon!=null)
        curWeapon.DisposeWeapon();

        objWeapon = Instantiate(newWeapon,this.transform);
        objWeapon.transform.position=transform.position;
        curWeapon = objWeapon.GetComponent<IWeapon>();
    }

}
