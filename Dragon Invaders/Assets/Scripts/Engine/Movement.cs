using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static void MoveBullets(List<Bullet> bulletList)
    {
        foreach (Bullet bullet in bulletList)
        {
            bullet.Move();
        }
    }

    public static void MoveEnemies(List<Enemy> enemyList)
    {
        foreach (Enemy enemy in enemyList)
        {
            enemy.Move();
        }
    }
}
