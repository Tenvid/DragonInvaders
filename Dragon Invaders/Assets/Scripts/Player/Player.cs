using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Actions
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [SerializeField] Rigidbody2D rb;

    //Attributes
    [SerializeField] float baseSpeed;
    [SerializeField] GameObject bulletPrefab;
    bool canShoot;
    [SerializeField]  float shootCooldown;
    float lastShoot;

    //Properties
    public Rigidbody2D Rigidbody
    {
        get { return rb; }
    }

    public InputAction Movement
    {
        get { return movement; }
        set { movement = value; }
    }

    public InputAction Fire
    {
        get { return fire; }
        set { fire = value; }
    }

    public bool CanShoot
    {
        get { return canShoot; }
        set { canShoot = value; }
    }
    public Vector2 movementSpeed
    {
        get { return movement.ReadValue<Vector2>(); }
    }

    public void Move()
    {
        rb.AddForce(new Vector2(baseSpeed * Time.deltaTime * movement.ReadValue<Vector2>().x, 0));
    }

    public void Shoot(List<Bullet> bullets, GameObject bulletsContainer)
    {
        if (Time.time - lastShoot > shootCooldown)
            canShoot = true;

        if(Fire.ReadValue<float>() > 0 && canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
            //bullet.GetComponent<Bullet>();
            //bullets.Add(Instantiate(bulletPrefab, transform.position, Quaternion.identity));
            bullet.transform.SetParent(bulletsContainer.transform);
            bullets.Add(bullet.GetComponent<Bullet>());
            canShoot = false;
            lastShoot = Time.time;
        }
    }
}
