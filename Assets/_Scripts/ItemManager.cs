using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private Controls controls;

    public Vector2 aimDir;
    public Joystick joystick;
    public GameObject button;
    public float aimDist = Mathf.Infinity;
    public LayerMask AimMask;

    public GameObject pHand;

    public bool holding;
    public product currentHold;
    public product lastP;
    public product p;
    public Aimable lastAim;
    public Aimable aimable;

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, aimDir, Color.red, 0.2f);

        #if UNITY_ANDROID
            joystick.gameObject.SetActive(true);
            button.SetActive(true);
            Aim(Vector2.zero, false, true);
        #endif

        AimAt();
    }

    void Awake()
    {
        controls = new Controls();

        controls.Player.AimPointer.performed += ctx => Aim(ctx.ReadValue<Vector2>(), true, false); 
        controls.Player.AimGamepad.performed += ctx => Aim(ctx.ReadValue<Vector2>(), false, false); 
        controls.Player.AimGamepad.canceled += ctx => Aim(ctx.ReadValue<Vector2>(), false, false);

        controls.Player.Pickup.performed += _ => Pickup();
    }

    public void AimAt()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir, aimDist, AimMask);

        if (hit.collider == null)
        {
            aimable = null;
        }
        else
        {
            aimable = hit.collider.GetComponent<Aimable>();
        }

        if (lastAim != aimable)
        {
            if (lastAim != null)
                lastAim.OnStopAim();
        }

        if (hit.collider == null)
            return;

        lastAim = aimable;

        aimable.OnAim(hit.point);
    }

    void Aim(Vector2 pos, bool isMouse, bool isTouch)
    {
        if (isTouch)
        {
            if (joystick.Direction == Vector2.zero)
                return;
            aimDir = joystick.Direction;
        } else
        {
            aimDir = pos;
        }
        
        if (isMouse)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos) - transform.position;
            aimDir = worldPos.normalized;
        }       
    }

    public void Pickup()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir, aimDist, AimMask);

        if (holding)
        { currentHold.holding = false; currentHold = null; holding = false; return; }

        if (hit.collider == null)
        { return; }

        if (hit.collider.GetComponent<product>() != null)
        {
            product p = hit.collider.GetComponent<product>();
            PickupItem(p);
        }
        else
        {
            product p = hit.collider.GetComponent<product>();
            Stand s = hit.collider.GetComponent<Stand>();
            s.Interact(this);
        }    
    }

    public void PickupItem(product p)
    {
        p.Hand = pHand.transform;
        p.holding = true;
        holding = true;
        currentHold = p;
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
