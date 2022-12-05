using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float riseSpeed;
    [SerializeField] Rigidbody2D rb;
 
    public Rigidbody2D Rigidbody 
    {
        get { return rb; }
        set { rb = value; }
    }
    
    public void Move()
    {
        //rb.AddForce(new Vector2(0, riseSpeed * Time.deltaTime));
        transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
    }
}
