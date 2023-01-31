using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PowerUp;

public class SpawnDespawnManager : MonoBehaviour
{
    public static void GenerateAnEnemy(GameObject freezerPrefab, GameObject enemyContainer, List<GameObject> enemies, Vector3 spawnPosition, int enemyPerRow, int rowNumber)
    {
        GameObject enemy = Instantiate(freezerPrefab, spawnPosition, Quaternion.identity);
        Main.aliveEnemiesCount++;
        enemies.Add(enemy);
        enemy.GetComponent<Enemy>().BaseSprite = enemy.GetComponent<SpriteRenderer>().sprite;
        enemy.transform.SetParent(enemyContainer.transform);
    }

    //Camera Size 2 --> cameraWidth = 2,25
    public static void MultipleEnemiesSpawn(Sprite freezerSprite, int enemyPerRow, int rowNumber, GameObject freezerPrefab, GameObject enemyContainer, List<GameObject> enemies, Vector3 spawnPosition)
    {
        for (int i = 1; i <= rowNumber; i++)
        {
            Vector3 spawnPos = CalculateSpawnPosition(enemyPerRow, rowNumber, spawnPosition);
            for (int j = 1; j <= enemyPerRow; j++)
            {
                if (Main.deadEnemies.Count == 0)
                    GenerateAnEnemy(freezerPrefab, enemyContainer, enemies, new Vector3(j * spawnPos.x - Constants.enemyXDifference, i * spawnPos.y, 0), enemyPerRow, rowNumber);
                else
                {
                    GameObject enemy = Main.deadEnemies.Last();
                    enemy.SetActive(true);
                    enemy.GetComponent<Enemy>().HasDied = false;
                    Main.deadEnemies.Remove(enemy);
                    Main.aliveEnemies.Add(enemy);
                    enemy.transform.position = new Vector3(j * spawnPos.x - Constants.enemyXDifference, i * spawnPos.y, 0);
                    enemy.GetComponent<Collider2D>().enabled = true;
                    enemy.GetComponent<Enemy>().GetComponent<Animator>().SetTrigger("resetEnemySprite");
                    Main.aliveEnemiesCount++;

                }
            }
        }
    }

    public static Vector3 CalculateSpawnPosition(float enemyPerRow, float rowNumber, Vector3 spawnPosition)
    {
        return new Vector3(spawnPosition.x / enemyPerRow, spawnPosition.y / rowNumber, 0);
    }

    //Generates the bullets at the start of the game
    public static void SetStartBullets(GameObject prefab, List<GameObject> bullets, GameObject container)
    {
        while (true)
        {
            if (bullets.Count > Constants.TotalBullets - 1)
                break;

            GameObject bullet = Instantiate(prefab, new Vector3(-5, -5, 0), Quaternion.identity);
            bullet.transform.SetParent(container.transform);
            bullets.Add(bullet);
            bullet.SetActive(false);
        }
    }

    //Stores all the bullets on screen
    public static void RemoveBullets(List<GameObject> storedBullets, List<GameObject> shotBullets, GameObject player)
    {
        while (true)
        {
            if (shotBullets.Count == 0)
                break;
            GameObject bullet = shotBullets.Last();
            storedBullets.Add(bullet);
            shotBullets.Remove(bullet);
            bullet.gameObject.SetActive(false);
        }
        player.GetComponent<Player>().ActualShoot = 0;
    }

    //Generates a PowerUp when an enemy dies
    public static void GeneratePowerUp(int x, int y, PowerUpType type)
    {
        Debug.Log("PowerUpSpawn");
    }
}
