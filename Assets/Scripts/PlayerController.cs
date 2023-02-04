using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Tilemaps;
using Assets.Scripts;

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
    private InputAction rotateAction;

    // Input Actions Value
    private Vector2 move;
    private float jump;
    private float fire;
    public Vector2 rotate;

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

    [Header("Current Weapon")]
    public GameObject objWeapon;
    public IWeapon curWeapon;

    //Player
    private Player player;

    //Math
    float a;
    int i = 1;
    
    public bool left=false;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rbody2D = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        player = GetComponent<Player>();
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
        }
    }
    private void FixedUpdate()
    {
        Jump();
        Move();
        RotateWeapon();
    }
    private void OnUpdate()
    {
        if (playerInput != null)
        { // Read actions from Input Manager
            moveAction = playerInput.actions["move"];
            fireAction = playerInput.actions["fire"];
            jumpAction = playerInput.actions["jump"];
            rotateAction = playerInput.actions["look"];
        }
        // Set value for Input Actions values
        move = moveAction.ReadValue<Vector2>();
        jump = jumpAction.ReadValue<float>();
        fire = fireAction.ReadValue<float>();
        rotate = rotateAction.ReadValue<Vector2>();

    }
    private void RotateWeapon()
    {
        if (objWeapon != null&&curWeapon.GetType()==EnumMeleeRanged.RANGED)
        {
            player.weaponInHand = true;
            float Angle;
            Quaternion quaternion;
            if ((rotate.x * i > 0 && rotate.y > 0) || (rotate.x * i > 0 && rotate.y < 0))
            {
                Angle = Mathf.Atan2(rotate.y, i * rotate.x);
                Angle = Angle * Mathf.Rad2Deg;
                quaternion = Quaternion.Euler(0, a, Angle);
                objWeapon.transform.rotation = quaternion;
            }





            /*if((objWeapon.transform.eulerAngles.z < 90 && objWeapon.transform.eulerAngles.z >=0)||(objWeapon.transform.eulerAngles.z < 360 && objWeapon.transform.eulerAngles.z > 270))
            {
                objWeapon.GetComponent<SpriteRenderer>().flipY = false;
            }
            else
            {
                objWeapon.GetComponent<SpriteRenderer>().flipY = true;
            }*/
        }
    }
    private void Move()
    {
        animator.SetFloat("speed", Mathf.Abs(move.x));
        rbody2D.velocity = new Vector2(move.x * moveSpeed, rbody2D.velocity.y);
        if (move.x > 0 || rotate.x>0)
        {
            Quaternion quaternion = new Quaternion(0, 0, 0, 0);
            transform.localRotation = quaternion;
            left = false;
            a = 0;
            i = 1;
        }
        if (move.x < 0 || rotate.x<0)
        {
            left= true;
            Quaternion quaternion = new Quaternion(0, 180, 0, 0);
            transform.localRotation = quaternion;
            a = 180;
            i = -1;
            // spriteBody.flipX = false;
            // spriteRenderer.flipX = false;
           // objWeapon.GetComponent<SpriteRenderer>().flipX = true;
        }


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
        //bool permableUp,permableDown;

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, permableLayerMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, permableLayerMask);
        if ((hitUp.collider != null) && (move.y > 0.9f) || (hitDown.collider != null) && (move.y < -0.9f))
        {
            coll.isTrigger = true;
        }
        else
        {
            coll.isTrigger = false;
        }

        /* permableUp = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + transform.localScale.y * 0.32f), 0.15f, permableLayerMask);
         permableDown = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - transform.localScale.y * 0.32f), 0.2f, permableLayerMask);
         if ((permableDown && move.y < -0.9f)||permableUp)
         {
             coll.isTrigger = true;
         }
         else
         {
             coll.isTrigger = false;
         }*/
    }
    private void Fire()
    {
        if (fire == 1)
        {
            if (curWeapon != null)
                curWeapon.Fire1();
        }
        else
        {
            if (curWeapon != null)
                curWeapon.Release();
        }

    }
    public void ChangeWeapon(GameObject newWeapon)
    {
        if (curWeapon != null)
            curWeapon.DisposeWeapon();

        objWeapon = Instantiate(newWeapon, transform.Find("body"));

        objWeapon.transform.position = transform.position - Vector3.forward;
        curWeapon = objWeapon.GetComponent<IWeapon>();
       
            Vector3 v=curWeapon.GetOffset();
            if(transform.localRotation.y>0)
            {
                objWeapon.transform.position = transform.position + new Vector3(-v.x, v.y, 0);
                objWeapon.transform.eulerAngles = new Vector3(0, 0, -v.z);
            }
            
            else
            {

                objWeapon.transform.position = transform.position + new Vector3(v.x, v.y, 0);
                objWeapon.transform.eulerAngles = new Vector3(0, 0, v.z);

            }
        
    }

}
