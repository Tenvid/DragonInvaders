using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float _descenseSpeed;
    [SerializeField] float _horizontalSpeed;

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

    public void SetRigidbody()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        transform.position = (new Vector3(transform.position.x + Time.deltaTime *  Mathf.Cos(Time.time) * _horizontalSpeed, transform.position.y - _descenseSpeed * Time.deltaTime, 0));
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.23f, 1.23f), transform.position.y, transform.position.z);
    }


}
