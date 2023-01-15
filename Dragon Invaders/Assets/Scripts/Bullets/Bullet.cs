using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //
    [SerializeField] float riseSpeed;
    [SerializeField] Rigidbody2D rb;
    bool _hasCollision = false;
    //

    

    public Rigidbody2D Rigidbody2D
    {
        get { return rb; }
        set { rb = value; }
    }



    //

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Freezer") || collision.gameObject.tag == "Limit")
        { 
            _hasCollision = true;
        }
    }

    public void Collision(List<GameObject> storedBullets, List<GameObject> shotBullets, GameObject player, ref int index)
    {
        if (_hasCollision)
        {
            this._hasCollision = false;
            storedBullets.Add(this.gameObject);
            shotBullets.RemoveAt(index);
            index--;
            player.GetComponent<Player>().ActualShoot--;
            this.gameObject.SetActive(false);
        }
    }

    public void Move()
    {
        //rb.AddForce(new Vector2(0, riseSpeed * Time.deltaTime));
        transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
    }

    public void SetRigidbody()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
