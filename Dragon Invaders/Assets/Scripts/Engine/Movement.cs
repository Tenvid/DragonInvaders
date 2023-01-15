using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Moves all bullets
    public static void MoveBullets(List<GameObject> bulletList)
    {
        for(int i = 0; i < bulletList.Count; i++)
        {
            bulletList[i].GetComponent<Bullet>().Move();
        }
    }
    //Moves all enemies
    //public static void MoveEnemies(List<Enemy> enemyList)
    //{
    //    foreach (Enemy enemy in enemyList)
    //    {
    //        enemy.Move();
    //    }
    //}
}
