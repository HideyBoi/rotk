using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Controls controls;

    public Rigidbody2D rb;
    public Animator animator;
    public float moveSpeed;

    public float holding;

    public Vector2 moveDir;
    public Joystick joystick;
    public Vector2 lastTouchMoveDir;



    void Awake()
    {
        controls = new Controls();

        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => Move(ctx.ReadValue<Vector2>());
    }


    void FixedUpdate()
    {
        #if UNITY_ANDROID
            joystick.gameObject.SetActive(true);
            
            if (joystick.Direction != lastTouchMoveDir)
            { Move(joystick.Direction); lastTouchMoveDir = joystick.Direction; }
            
        #endif

        animator.SetFloat("Horizontal", moveDir.x);
        animator.SetFloat("Vertical", moveDir.y);
        animator.SetFloat("Speed", (moveDir * moveSpeed).sqrMagnitude);
        rb.AddForce(moveDir * moveSpeed);
    }

    void Move(Vector2 stickDir)
    {
        moveDir = stickDir;
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
