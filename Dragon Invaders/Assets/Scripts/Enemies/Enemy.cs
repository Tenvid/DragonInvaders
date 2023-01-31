using System;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float _descenseSpeed;
    [SerializeField] float _horizontalSpeed;
    bool _hasDied = false;
    [SerializeField] Sprite _baseSprite;
    //

    public bool HasDied
    {
        get { return _hasDied; }
        set { _hasDied = value; }
    }
    public Sprite BaseSprite
    {
        get { return _baseSprite; }
        set { _baseSprite = value; }
    }

    public bool TestCollision(LayerMask mask)
    {
        if (rb.IsTouchingLayers(mask))
            return true;
        return false;
    }

    //

    public void MoveToDead()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            _hasDied = true;
        }
    }
    public void Explode()
    {
        GetComponent<Animator>().SetTrigger("isExploding");
    }

    public void Dead(int index)
    {
        if (_hasDied)
        {
            SpawnDespawnManager.RemoveBullets(Main.storedBullets, Main.shotBullets, Main.player.gameObject);
            Debug.Log("Enemigo muere");
            //this.gameObject.tag = "Untagged";
            Main.deadEnemies.Add(this.gameObject);
            Main.aliveEnemies.RemoveAt(index);
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            Main.aliveEnemiesCount--;
            Main.player.KilledEnemies++;
            _hasDied = false;
            Explode();
        }
    }

    public void Move()
    {
        transform.position = (new Vector3(transform.position.x + Time.deltaTime *  Mathf.Cos(Time.time) * _horizontalSpeed, transform.position.y - _descenseSpeed * Time.deltaTime, 0));
    }
}
