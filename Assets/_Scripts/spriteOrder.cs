using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour
{
    public bool onlyOnce;

    public bool isBeingHeld;

    void LateUpdate()
    {
        if (isBeingHeld)
            return;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        if (onlyOnce)
            Destroy(this);
    }
}
