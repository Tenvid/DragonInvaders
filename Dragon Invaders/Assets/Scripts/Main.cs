using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

delegate GameObject generator();

public class Main : MonoBehaviour
{
    //public static List<GameObject> deadEnemies = new List<GameObject>();
    /// <summary>
    /// Main character that shoots
    /// </summary>
    public static Player player;

    public static int aliveEnemiesCount = 0;
    public static List<GameObject> chargedEnemies = new List<GameObject>();
    public static List<GameObject> chargedBullets = new List<GameObject>();
    public static ObjectPool<GameObject> bullets;
    public static ObjectPool<GameObject> enemies;
    public GameObject bulletsContainer;
    public GameObject enemyContainer;
    public Sprite freezerSprite;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI lifesLabel;



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

    //TEMP ZONE
    public PowerUp powerUp;
    [SerializeField] TextMeshProUGUI bulletsCounter;
    string bulletCounterText;
    //

    //Activates inputs
    private void OnEnable()
    {
        player.movement.Enable();
        player.fire.Enable();
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        powerUp = FindObjectOfType<PowerUp>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.CanShoot = true;
        player.ActualShoot = 0;
        _waveCounter = 0;
        _spawnPosition.Set(pivotUpLeft.transform.position.x, pivotUpLeft.transform.position.y, 0);
        //SpawnDespawnManager.SetStartBullets(_bulletPrefab, storedBullets, bulletsContainer);

        bullets = new(() =>
        {
            var bullet = Instantiate(_bulletPrefab, new Vector3(player.transform.position.x, player.transform.position.y + Constants.BulletSpawnDifference, 0), Quaternion.identity);
            chargedBullets.Add(bullet);
            Debug.Log("BulletCreated");
            bullet.transform.SetParent(bulletsContainer.transform);
            return bullet;
        }, (bullet) =>
        {
            Debug.Log("BulletActivated");
            bullet.SetActive(true);
        }, (bullet) =>
        {

            bullet.SetActive(false);
        },
        (bullet) =>
        {
            chargedBullets.Remove(bullet);
            Destroy(bullet);
        }, true, 0, 30);


        enemies = new(() =>
        {
            var enemy = Instantiate(_freezerPrefab);
            chargedEnemies.Add(enemy);
            enemy.transform.SetParent(enemyContainer.transform);
            return enemy;
        },
        (enemy) =>
        {
            enemy.SetActive(true);
        }, (enemy) =>
        {
            enemy.SetActive(false);
        },
        (enemy) =>
        {
            Destroy(enemy);
        }, true, 0, 30);
    }

    // Update is called once per frame
    void Update()
    {
        bulletCounterText = $"ChargedBullets: {chargedBullets.Count}, AliveEnemies: {chargedEnemies.Count}";
        bulletsCounter.text = bulletCounterText;

        TextManager.SetScoreText(player.KilledEnemies * Constants.scoreMultiplier, scoreLabel);
        player.Move();
        //Moves bullets to shot list and adds playerShots
        if (player.fire.ReadValue<float>() > 0)
            player.Shoot(chargedBullets, _bulletPrefab);


        //Manages the enemy waves
        try
        {
            if (aliveEnemiesCount < 1)
            {
                SpawnDespawnManager.RemoveBullets(chargedBullets);
                _waveCounter++;
                switch (_waveCounter)
                {
                    case 1:
                        SpawnDespawnManager.MultipleEnemiesSpawn(freezerSprite, 5, 3, _freezerPrefab, enemyContainer, chargedEnemies, _spawnPosition);
                        break;
                    case 2:
                        SpawnDespawnManager.MultipleEnemiesSpawn(freezerSprite, 2, 7, _freezerPrefab, enemyContainer, chargedEnemies, _spawnPosition);
                        break;
                    case 3:
                        SpawnDespawnManager.MultipleEnemiesSpawn(freezerSprite, 15, 2, _freezerPrefab, enemyContainer, chargedEnemies, _spawnPosition);
                        break;
                    default:
                        _waveCounter = 0;
                        break;
                }
            }
        }
        catch (System.Exception)
        {
            throw new System.Exception("Error con las oleadas");
        }

        try
        {
            if (chargedBullets.Count > 0)
                //Moves all the shot bullets
                Movement.MoveBullets(chargedBullets);
        }
        catch (System.Exception)
        {
            throw new System.Exception("No hay balas para mover");
        }

        //Moves the enemies and makes them to disappear when a bullet hits
        try
        {
            for (int i = 0; i < chargedEnemies.Count; i++)
            {
                //chargedEnemies[i].GetComponent<Enemy>().Dead(i);
                try
                {
                    chargedEnemies[i].GetComponent<Enemy>().Move();
                }
                catch
                {
                    Debug.Log(i--);
                }
                //if (chargedEnemies[i].activeSelf)

                //SpawnDespawnManager.BulletImpact(player, shotBullets, storedBullets, enemies, i);
            }
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }
}
