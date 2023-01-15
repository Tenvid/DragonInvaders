using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public const float enemyXDifference = -0.7308f;
    //Height difference between player and bullets at spawn
    public const float BulletSpawnDifference = 0.3f;
    public const int TotalBullets = 10;
    public const int scoreMultiplier = 200;
    public const float PlayerShootCooldown = 0.7f;
}
