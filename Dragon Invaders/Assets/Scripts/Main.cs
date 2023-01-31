using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static List<GameObject> aliveEnemies = new List<GameObject>();
    public static List<GameObject> deadEnemies = new List<GameObject>();
    public static Player player;
    public static int aliveEnemiesCount = 0;
    public static List<GameObject> shotBullets = new List<GameObject>();
    public static List<GameObject> storedBullets = new List<GameObject>();
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
        SpawnDespawnManager.SetStartBullets(_bulletPrefab, storedBullets, bulletsContainer);
    }

    // Update is called once per frame
    void Update()
    {
        bulletCounterText = $"ShotBullets: {shotBullets.Count}, StoredBullets: {storedBullets.Count}, AliveEnemies: {aliveEnemies.Count}, DeadEnemies: {deadEnemies.Count}";
        bulletsCounter.text = bulletCounterText;

        TextManager.SetScoreText(player.KilledEnemies * Constants.scoreMultiplier, scoreLabel);
        player.Move();
        //Moves bullets to shot list and adds playerShots
        if(player.fire.ReadValue<float>() > 0)
            player.Shoot(storedBullets, shotBullets, _bulletPrefab);

        //Moves all the shot bullets
        Movement.MoveBullets(shotBullets);

        for(int i = 0; i < shotBullets.Count; i++)
        {
            shotBullets[i].GetComponent<Bullet>().Collision(storedBullets, shotBullets, player.gameObject, ref i);
        }
        //Moves the enemies and makes them to disappear when a bullet hits
        for (int i = 0; i < aliveEnemies.Count; i++)
        {
            aliveEnemies[i].GetComponent<Enemy>().Move();
            aliveEnemies[i].GetComponent<Enemy>().Dead(i);
            //SpawnDespawnManager.BulletImpact(player, shotBullets, storedBullets, enemies, i);
        }
        //Manages the enemy waves
        if (aliveEnemies.Count < 1)
        {
            SpawnDespawnManager.RemoveBullets(storedBullets, shotBullets, player.gameObject);
            _waveCounter++;
            switch (_waveCounter)
            {
                case 1:
                    SpawnDespawnManager.MultipleEnemiesSpawn(freezerSprite, 5, 3, _freezerPrefab, enemyContainer, aliveEnemies, _spawnPosition);
                    break;
                case 2:
                    SpawnDespawnManager.MultipleEnemiesSpawn(freezerSprite, 2, 7, _freezerPrefab, enemyContainer, aliveEnemies, _spawnPosition);
                    break;
                case 3:
                    SpawnDespawnManager.MultipleEnemiesSpawn(freezerSprite, 15, 2, _freezerPrefab, enemyContainer, aliveEnemies, _spawnPosition);
                    break;
                default:
                    _waveCounter = 0;
                    break;
            }
        }
    }
}
