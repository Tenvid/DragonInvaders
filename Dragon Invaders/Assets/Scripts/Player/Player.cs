using System.Collections.Generic;
using System.Linq;
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
    //[SerializeField] GameObject bulletPrefab;
    bool _canShoot;
    [SerializeField] float shootCooldown;
    float _lastShootTime;
    [SerializeField] int _currentShoot = 0;

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
        get { return _canShoot; }
        set { _canShoot = value; }
    }
    public Vector2 movementSpeed
    {
        get { return movement.ReadValue<Vector2>(); }
    }

    public int ActualShoot
    {
        get { return _currentShoot; }
        set
        {
            _currentShoot = value;
            if (_currentShoot < 0)
                _currentShoot = 0;
        }
    }

    //Functions
    public void Move()
    {
        rb.AddForce(new Vector2(baseSpeed * movement.ReadValue<Vector2>().x * Time.deltaTime, 0));
    }

    public void Shoot(List<Bullet> shotBullets, List<Bullet> storedBullets, GameObject bulletsContainer)
    {
        if (ActualShoot < Constants.TotalBullets)
        {
            if (Time.time - _lastShootTime > shootCooldown)
                _canShoot = true;

            if (Fire.ReadValue<float>() > 0 && _canShoot)
            {
                shotBullets.Add(storedBullets[storedBullets.Count - 1]);
                storedBullets.RemoveAt(storedBullets.Count - 1);
                shotBullets.Last().transform.position = new Vector3(transform.position.x, transform.position.y + Constants.BulletSpawnDifference, 0);
                shotBullets.Last().GetComponent<SpriteRenderer>().enabled = true;

                _currentShoot++;
                _canShoot = false;
                _lastShootTime = Time.time;
            }
        }
    }
}
