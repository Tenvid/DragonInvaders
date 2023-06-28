using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    //Actions
    public InputAction movement;
    public InputAction fire;

    //Attributes
    bool _canShoot;
    float _lastShootTime;
    [SerializeField] float baseSpeed;
    [SerializeField] float shootCooldown;
    [SerializeField] int _currentShoot = 0;
    int killedEnemies;
    int totalLifes;
    //[SerializeField] float _powerUpTimer;
    int _score;
    //Properties

    public int Score
    {
        get; set;
    }

    public bool CanShoot
    {
        get
        {
            return _canShoot;
        }
        set
        {
            _canShoot = value;
        }
    }
    public Vector2 movementSpeed
    {
        get
        {
            return movement.ReadValue<Vector2>();
        }
    }

    public int ActualShoot
    {
        get
        {
            return _currentShoot;
        }
        set
        {
            _currentShoot = value;
            if (_currentShoot < 0)
                _currentShoot = 0;
        }
    }

    public int KilledEnemies
    {
        get;
        set;
    }

    public int TotalLifes
    {
        get;
        set;
    }

    //Functions

    //public void DiscountPowerUpTime()
    //{
    //    _powerUpTimer -= Time.deltaTime;
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
        
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag.ToString());
        if (collision.gameObject.tag == "Freezer")
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        if (collision.gameObject.CompareTag("Limit"))
            //GetComponent<CharacterController>().Move(- new Vector3(baseSpeed * movement.ReadValue<Vector2>().x * Time.deltaTime, 0, 0));
            Debug.Log("borde");
        if (collision.gameObject.tag == "powerUp")
        {
            //_powerUpTimer = 60;
        }
    }
    //Flips character sprite
    public void SetOrientation()
    {
        if (movement.ReadValue<Vector2>().x > 0)
            this.GetComponentInChildren<SpriteRenderer>().flipX = true;
        else if (movement.ReadValue<Vector2>().x < 0)
            this.GetComponentInChildren<SpriteRenderer>().flipX = false;
    }
    //Moves the player and orientates the sprite
    public void Move()
    {
        //rb.AddForce(new Vector2(baseSpeed * movement.ReadValue<Vector2>().x * Time.deltaTime, 0));
        GetComponent<Rigidbody2D>().AddForce(new Vector3(baseSpeed * movement.ReadValue<Vector2>().x * Time.deltaTime, 0, 0));
        //this.transform.position = new Vector3 (GetComponentInChildren<Transform>().position.x, GetComponentInChildren<Transform>().position.y, GetComponentInChildren<Transform>().position.z);
        SetOrientation();
    }
    //Activates the shoot depending on the cooldown
    public void EnableShoot()
    {
        //Cooldown
        if (Time.time - _lastShootTime >= Constants.PlayerShootCooldown)
            _canShoot = true;
    }

    //Makes the player shoot a bullet
    public void Shoot(List<GameObject> shotBullet, GameObject bulletPrefab)
    {
        //If there are stored bullets
        //if (ActualShoot >= Constants.TotalBullets)
        //    return;
        EnableShoot();
        //If Fire button is pressed and player can shot
        if (_canShoot)
        {
            //Makes the player to know that has already shoot
            _canShoot = false;
            _lastShootTime = Time.time;

            GameObject bullet = Main.bullets.Get();
            //bullet.SetActive(true);
            //shotBullet.Add(bullet);
            //storedBullets.Remove(bullet);

            bullet.transform.position = new Vector3(transform.position.x, transform.position.y + Constants.BulletSpawnDifference, 0);
            _currentShoot++;
        }
    }
}
