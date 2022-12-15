using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Bullet() { }

    public Bullet(Rigidbody2D rb, bool enabled)
    {
        this.rb = rb;
        this.GetComponent<SpriteRenderer>().enabled = enabled;
    }

    [SerializeField] float riseSpeed;
    [SerializeField] Rigidbody2D rb;

    public Rigidbody2D Rigidbody2D
    {
        get { return rb; }
        set { rb = value; }
    }

    public void Move()
    {
        //rb.AddForce(new Vector2(0, riseSpeed * Time.deltaTime));
        transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
    }

    public bool TestCollision(LayerMask mask)
    {
        if (rb.IsTouchingLayers(mask))
            return true;
        return false;
    }

    public Bullet Create(GameObject bulletsContainer)
    {
        //GetComponent<Bullet>().Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        transform.SetParent(bulletsContainer.transform);
        enabled = false;
        return this;
    }

    public void SetRigidbody()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
