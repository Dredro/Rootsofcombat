
using UnityEngine;

using UnityEngine.InputSystem;

using Assets.Scripts;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public EnumPlayerColor PlayerColor;
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
    private bool mGrounded;
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

    [Header("AudioClip")]
    public AudioSource audioSource;
    public AudioClip jumpClip;

    public GameObject pauseUi;

    //Player
    private Player player;
    private bool mFacingRight = true; 
    private Vector3 mVelocity = Vector3.zero;
    [FormerlySerializedAs("mJumpForce")] [SerializeField] private float mJumpForce = 400f;	
    [FormerlySerializedAs("mMovementSmoothing")] [Range(0, .3f)] [SerializeField] private float mMovementSmoothing = .05f;
    //Math
    float a;
    int i = 1;
    
    public bool left=false;
    
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rbody2D = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        player = GetComponent<Player>();
        GetActions();
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
        // Set value for Input Actions values
        move = moveAction.ReadValue<Vector2>();
        jump = jumpAction.ReadValue<float>();
        fire = fireAction.ReadValue<float>();
        rotate = rotateAction.ReadValue<Vector2>();
       

    }

    private void GetActions()
    {
        // Read actions from Input Manager
        moveAction = playerInput.actions["move"];
        fireAction = playerInput.actions["fire"];
        jumpAction = playerInput.actions["jump"];
        rotateAction = playerInput.actions["look"];
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
        else
        {
            player.weaponInHand = false;
        }
    }

    public void Move()
    {
        // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move.x * moveSpeed, rbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            rbody2D.velocity = Vector3.SmoothDamp(rbody2D.velocity, targetVelocity, ref mVelocity, mMovementSmoothing);
            animator.SetFloat("speed", Mathf.Abs(move.x*moveSpeed));
            // If the input is moving the player right and the player is facing left...
            if (move.x > 0 && !mFacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move.x < 0 && mFacingRight)
            {
                // ... flip the player.
                Flip();
            }
    }

    private void Flip()
    {
        mFacingRight = !mFacingRight;
       
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void GroundCheck()
    {
        mGrounded = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2), 0.05f, layerMask);
    }

    private void Jump()
    {
        if ((mGrounded) && (jump == 1) && (Time.time > jumpTimer))
        {
            audioSource.PlayOneShot(jumpClip);
            rbody2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            jumpTimer = Time.time + jumpDelay;
        }
    }
    private void Descent()
    {
        //bool permableUp,permableDown;

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, permableLayerMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, permableLayerMask);
        if ((hitUp.collider != null) && (jump==1) || (hitDown.collider != null) && (move.y < -0.9f))
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
            {
                curWeapon.Fire1();
            }
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
        curWeapon.SetPlayer(PlayerColor);
        Vector3 v=curWeapon.GetOffset();
            if(transform.localRotation.y>0)
            {
                objWeapon.transform.position = transform.position + new Vector3(-v.x, v.y, -1);
                objWeapon.transform.eulerAngles = new Vector3(0, 0, -v.z);
            }
            
            else
            {

                objWeapon.transform.position = transform.position + new Vector3(v.x, v.y, -1);
                objWeapon.transform.eulerAngles = new Vector3(0, 0, v.z);

            }
        
    }

}
