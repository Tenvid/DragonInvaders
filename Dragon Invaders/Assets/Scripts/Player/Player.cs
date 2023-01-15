using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField] float _powerUpTimer;
    int _score;
    //Properties

    public int Score
    {
        get; set;
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

    public void DiscountPowerUpTime()
    {
        _powerUpTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Freezer"))
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        if (collision.gameObject.CompareTag("Limit"))
            //GetComponent<CharacterController>().Move(- new Vector3(baseSpeed * movement.ReadValue<Vector2>().x * Time.deltaTime, 0, 0));
            Debug.Log("borde");
        if (collision.gameObject.CompareTag("powerUp"))
        {
            _powerUpTimer = 60;
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
    public void Move()
    {
        //rb.AddForce(new Vector2(baseSpeed * movement.ReadValue<Vector2>().x * Time.deltaTime, 0));
        GetComponent<Rigidbody2D>().AddForce(new Vector3(baseSpeed * movement.ReadValue<Vector2>().x * Time.deltaTime, 0, 0));
        //this.transform.position = new Vector3 (GetComponentInChildren<Transform>().position.x, GetComponentInChildren<Transform>().position.y, GetComponentInChildren<Transform>().position.z);
        SetOrientation();
    }

    //Makes the player shoot a bullet
    public void Shoot(List<GameObject> storedBullets, List<GameObject> shotBullet, GameObject bulletPrefab)
    {
        //If there are stored bullets
        if (ActualShoot < Constants.TotalBullets)
        {
            //Cooldown
            if (Time.time - _lastShootTime >= Constants.PlayerShootCooldown)
                _canShoot = true;

            //If Fire button is pressed and player can shot
            if (_canShoot)
            {
                //Makes the player to know that has already shoot
                
                _canShoot = false;
                _lastShootTime = Time.time;

                GameObject bullet = storedBullets[Constants.TotalBullets - ActualShoot - 1];
                bullet.SetActive(true);
                bullet.tag = "Bullet";
                shotBullet.Add(bullet);
                storedBullets.Remove(bullet);
                bullet.transform.position = transform.position + new Vector3(0, Constants.BulletSpawnDifference, 0);
                _currentShoot++;
            }
        }
    }
}
