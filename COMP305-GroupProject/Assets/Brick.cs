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
    private int _hp = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void BeingHit(ProjectileData data)
    {
        // do the logic here
        _hp--;
        if (_hp == 1)
        {
            _brickRenderer.sprite = _brickImages[_hp];
        }
        if (_hp == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
