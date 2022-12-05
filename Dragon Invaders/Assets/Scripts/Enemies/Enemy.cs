using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    public Rigidbody2D Rigidbody2D
    {
        get { return rb; }
        set { rb = value; }
    }

    public bool TestCollision(LayerMask mask)
    {
        if (rb.IsTouchingLayers(mask))
            return true;
        return false;
    }
}
