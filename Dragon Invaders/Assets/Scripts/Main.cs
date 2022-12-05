using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Player player;
    public List<Bullet> bullets;
    public GameObject bulletsContainer;
    Enemy enemy;

    //[SerializeField] LayerMask bulletLayer;

    private void OnEnable()
    {
        player.Movement.Enable();
        player.Fire.Enable();
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.CanShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        player.Move();
        player.Shoot(bullets, bulletsContainer);
        foreach(Bullet bullet in bullets)
        {
            bullet.Move();
            //bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0));
        }
        //if (enemy.TestCollision(bulletLayer))
        //{
        //    Destroy(enemy);
            
        //}
    }
}
