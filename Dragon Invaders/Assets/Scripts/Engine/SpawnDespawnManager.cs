using System.Collections.Generic;
using UnityEngine;

public class SpawnDespawnManager : MonoBehaviour
{
    public static void GenerateAnEnemy(GameObject freezerPrefab, GameObject enemyContainer, List<Enemy> enemies, Vector3 spawnPosition, int enemyPerRow, int rowNumber)
    {
        GameObject enemy = Instantiate(freezerPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<Enemy>().SetRigidbody();
        enemies.Add(enemy.GetComponent<Enemy>());
        enemy.transform.SetParent(enemyContainer.transform);
    }

    //Camera Size 2 --> cameraWidth = 2,25
    public static void MultipleEnemiesSpawn(int enemyPerRow, int rowNumber, GameObject freezerPrefab, GameObject enemyContainer, List<Enemy> enemies, Vector3 spawnPosition)
    {
        for (int i = 1; i <= rowNumber; i++)
        {
            for (int j = 1; j <= enemyPerRow; j++)
            {
                GenerateAnEnemy(freezerPrefab, enemyContainer, enemies, new Vector3(j * (spawnPosition.x / enemyPerRow) - Constants.enemyXDifference, i * (spawnPosition.y / rowNumber), 0), enemyPerRow, rowNumber);
            }
        }
    }

    public static void KillEnemies(List<Enemy> enemies, LayerMask bulletLayer, int worldEnemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].TestCollision(bulletLayer))
            {
                Destroy(enemies[i].gameObject);
                enemies.RemoveAt(i);
                i--;
                worldEnemies--;
            }
        }
    }

    //public static void BulletImpact(Player player, List<Bullet> shotBullets, List<Bullet> delayedBullets, List<Enemy> enemies, int enemyIndex)
    //{
    //    for (int i = 0; i < shotBullets.Count; i++)
    //    {
    //        if (shotBullets[i].Rigidbody2D.IsTouching(enemies[enemyIndex].GetComponent<Collider2D>()))
    //        {
    //            //delayedBullets.Add(shotBullets[i]);
    //            Destroy(enemies[i]);
    //            //shotBullets.Remove(delayedBullets.Last());
    //            enemies.RemoveAt(enemyIndex);
    //            delayedBullets.Add(shotBullets[i]);
    //            shotBullets.RemoveAt(i);

    //            player.ActualShoot--;
    //            enemyIndex--;
    //            i--;
    //        }
    //    }
    //}
    public static void BulletImpact(Player player, List<Bullet> shotBullets, List<Bullet> storedBullets, List<Enemy> enemies, int enemyIndex)
    {
        for (int i = 0; i < shotBullets.Count; i++)
        {
            if (shotBullets[i].Rigidbody2D.IsTouching(enemies[enemyIndex].GetComponent<Collider2D>()))
            {
                Destroy(enemies[enemyIndex].gameObject);
                enemies.RemoveAt(enemyIndex);
                if (enemyIndex > 0)
                    enemyIndex--;

                storedBullets.Add(shotBullets[i]);
                shotBullets.RemoveAt(i);
                if (i > 0)
                    i--;

                player.ActualShoot--;
            }
        }
    }
    public static void KillPlayer(Player player, LayerMask layer)
    {
        if (player.Rigidbody.IsTouchingLayers(layer))
        {
            Application.Quit();
            Destroy(player.gameObject);
        }
    }

    public static void SetStartBullets(GameObject prefab, List<Bullet> bullets, GameObject container)
    {
        while (true)
        {
            if (bullets.Count > Constants.TotalBullets - 1)
                break;

            GameObject bullet = Instantiate(prefab, new Vector3(-5, -5, 0), Quaternion.identity);
            bullet.GetComponent<SpriteRenderer>().enabled = true;
            bullet.AddComponent<Bullet>();
            bullet.GetComponent<Bullet>().SetRigidbody();
            bullet.transform.SetParent(container.transform);
            bullets.Add(bullet.GetComponent<Bullet>());
        }
    }
    public static void RemoveBullets(List<Bullet> storedBullets, List<Bullet> shotBullets)
    {
        while (true)
        {
            if (shotBullets.Count == 0)
                break;

            storedBullets.Add(shotBullets[0]);
            shotBullets.RemoveAt(0);
        }
    }
}
