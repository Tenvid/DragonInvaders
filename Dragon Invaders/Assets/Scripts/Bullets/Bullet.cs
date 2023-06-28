using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //
    /// <summary>
    /// Speed oof the bullet when moves (Declared on Unity)
    /// </summary>
    [SerializeField] float riseSpeed;
    /// <summary>
    /// It says if the bullet has collisioned
    /// </summary>
    bool _hasCollision = false;
    //

    /// <summary>
    /// Actions to do depending on the tag of the object collides with This
    /// </summary>
    /// <param name="collision">Tag of the object that collides with This</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollision)
        {
            _hasCollision = false;
            return;
        }
        if (collision.gameObject.CompareTag("Freezer") || collision.gameObject.tag == "Limit")
        { 
            _hasCollision = true;
            Debug.Log("Bala choca");
            Main.player.GetComponent<Player>().ActualShoot--;
            Main.bullets.Release(this.gameObject);
        }
    }
    /// <summary>
    /// Displaces the bullet up
    /// </summary>
    public void Move()
    {
        //rb.AddForce(new Vector2(0, riseSpeed * Time.deltaTime));
        transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
    }
}
