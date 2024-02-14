using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ProjectileData
{
    public float damage;
    public bool isPlayer;

    public ProjectileData(float damage, bool isPlayer)
    {
        this.damage = damage;
        this.isPlayer = isPlayer;
    }
}

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] bool isPlayer;
    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(bool isPlayer, float damage, float speed = 0)
    {
        this.isPlayer = isPlayer;
        this.damage = damage;
        this.speed = speed == 0 ? this.speed : speed;
    }

    public void Shot(Vector2 direction)
    {
        if (body.velocity.magnitude > 0) return;

        SoundManager.Instance.PlaySound("Shoot");
        body.AddForce(direction * this.speed);
    }

    public float GetDamage() 
    {
        return damage;
    }

    [ContextMenu("Do Something")]
    void AddForce()
    {
        body.AddForce(Vector2.up * speed);
        Debug.Log("Perform operation");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SendMessage("BeingHit", new ProjectileData(damage, isPlayer), SendMessageOptions.DontRequireReceiver);

        var effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound("Explosion");
        effect.transform.localScale *= transform.localScale.x;
        Destroy(gameObject);
    }
}
