using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _brickImages;
    [SerializeField]
    private SpriteRenderer _brickRenderer;

    [SerializeField]
    private float _hp = 150.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Projectile bullet = collision.gameObject.GetComponent<Projectile>();
            _hp -= bullet.GetDamage();
            if (_hp >= 90)
            {
                _brickRenderer.sprite = _brickImages[1];
            }
            if (_hp <= 50)
            {
                _brickRenderer.sprite = _brickImages[0];
            }
            if (_hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void BeingHit(ProjectileData data)
    {
        // do the logic here
        _hp -= data.damage;
        if (_hp >= 90)
        {
            _brickRenderer.sprite = _brickImages[1];
        }
        if (_hp <= 50)
        {
            _brickRenderer.sprite = _brickImages[0];
        }
        if (_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
