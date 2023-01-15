using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        SPEEDPLAYER,
        SPEEDFIRE,
        REPULSIVE,
        NULL,
    }

    public PowerUpType type;
    Color _color;
    public PowerUp(int x, int y, PowerUpType type, Color color)
    {
        _color = color;
        transform.position = new Vector3(x, y, 0);
        this.type = type;
    }
}
