using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private Controls controls;

    public Vector2 aimDir;
    public float aimDist = Mathf.Infinity;
    public LayerMask AimMask;

    public GameObject pHand;

    public bool holding;
    public product currentHold;
    public product lastP;
    public product p;

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, aimDir, Color.red, 1f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir, aimDist, AimMask);

        

        if (hit.collider == null)
        {
            p = null;
        } else
        {
            p = hit.collider.GetComponent<product>();
        }

        if (lastP != p)
        {
            if (lastP != null)
                lastP.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (hit.collider == null)
            return;

        lastP = p;

        p.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    void Awake()
    {
        controls = new Controls();

        controls.Player.AimPointer.performed += ctx => Aim(ctx.ReadValue<Vector2>(), true); 
        controls.Player.AimGamepad.performed += ctx => Aim(ctx.ReadValue<Vector2>(), false); 
        controls.Player.AimGamepad.canceled += ctx => Aim(ctx.ReadValue<Vector2>(), false);

        controls.Player.Pickup.performed += _ => Pickup();
    }

    void Aim(Vector2 pos, bool isMouse)
    {
        aimDir = pos;

        if (isMouse)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos) - transform.position;
            aimDir = worldPos.normalized;
        }
            
    }

    void Pickup()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir, aimDist, AimMask);

        if (holding)
        { Debug.Log("Holding!"); currentHold.holding = false; currentHold = null; holding = false; return; }

        if (hit.collider == null)
        { Debug.Log("Missed!"); return; }

        product p = hit.collider.GetComponent<product>();

        p.Hand = pHand.transform;
        p.holding = true;
        holding = true;
        currentHold = p;

        Debug.Log("Attempting to pickup: " + p.itemName);
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
