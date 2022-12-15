using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Player player;
    public List<Bullet> shotBullets = new List<Bullet>();
    public List<Bullet> storedBullets = new List<Bullet>();
    public List<Enemy> enemies = new List<Enemy>();
    public GameObject bulletsContainer;
    public GameObject enemyContainer;

    //internal static float spawnTime;
     Vector3 _spawnPosition = new Vector3();
    [SerializeField] GameObject _freezerPrefab;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] int _waveCounter;

    [SerializeField] int enemyPerRow;
    [SerializeField] int totalRows;

    public GameObject pivotUpLeft;
    public GameObject pivotUpRight;
    public GameObject pivotDownLeft;
    public GameObject pivotDownRight;
    public GameObject topLimit;

    public LayerMask bulletLayer;
    public LayerMask enemyLayer;
    private void OnEnable()
    {
        player.Movement.Enable();
        player.Fire.Enable();
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.CanShoot = true;
        player.ActualShoot = 0;
        _spawnPosition.Set(pivotUpLeft.transform.position.x, pivotUpLeft.transform.position.y, 0);
        _waveCounter = 0;
        //for (int i = 0; i < 10; i++)
        //{
        //    GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        //    bullet.GetComponent<SpriteRenderer>().enabled = false;
        //    bullet.transform.SetParent(bulletsContainer.transform);
        //    delayedBullets.Add(bullet.GetComponent<Bullet>());
        //    //delayedBullets[i].Rigidbody2D = GetComponent<Rigidbody2D>();
        //    //delayedBullets[i].GetComponent<Bullet>().Rigidbody2D = delayedBullets[i].GetComponent<Rigidbody2D>();
        //}

        SpawnDespawnManager.SetStartBullets(_bulletPrefab, storedBullets, bulletsContainer);
    }

    // Update is called once per frame
    void Update()
    {
        player.Move();
        player.Shoot(shotBullets, storedBullets, bulletsContainer);
        LimitMovements.LimitTopBullets(player, topLimit, storedBullets, shotBullets);
        Movement.MoveBullets(shotBullets);
        //Movement.MoveEnemies(enemies);
        //SpawnDespawnManager.MultipleEnemiesSpawn(enemyPerRow, totalRows, _freezerPrefab, enemyContainer, enemies, spawnPosition);
        //SpawnDespawnManager.KillEnemies(enemies, bulletLayer, enemyCounter);
        //SpawnDespawnManager.KillBullets(bullets, enemyLayer, bulletCounter);

        if (enemies.Count == 0)
        {
            _waveCounter++;
            SpawnDespawnManager.RemoveBullets(storedBullets, shotBullets);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Move();
            SpawnDespawnManager.BulletImpact(player, shotBullets, storedBullets, enemies, i);
        }

        for(int i = 0; i < storedBullets.Count; i++)
        {
            storedBullets[i].transform.position = new Vector3(-5, -5, 0);
        }

        SpawnDespawnManager.KillPlayer(player, enemyLayer);

        if (enemies.Count < 1)
        {
            switch (_waveCounter)
            {
                case 1:
                    SpawnDespawnManager.MultipleEnemiesSpawn(5, 2, _freezerPrefab, enemyContainer, enemies, _spawnPosition);
                    break;
                case 2:
                    SpawnDespawnManager.MultipleEnemiesSpawn(2, 7, _freezerPrefab, enemyContainer, enemies, _spawnPosition);
                    break;
                case 3:
                    SpawnDespawnManager.MultipleEnemiesSpawn(15, 2, _freezerPrefab, enemyContainer, enemies, _spawnPosition);
                    break;
                default:
                    _waveCounter = 0;
                    break;
            }
        }
        _spawnPosition.Set(pivotUpLeft.transform.position.x, pivotUpLeft.transform.position.y, 0);

    }
}
