using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float damage;
    [SerializeField] float speed;
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

    public void Setup(float damage, float speed = 0)
    {
        this.damage = damage;
        this.speed = speed == 0 ? this.speed : speed;
    }

    public void Shot(Vector2 direction)
    {
        if (body.velocity.magnitude > 0) return;

        body.AddForce(direction * this.speed);
    }

    [ContextMenu("Do Something")]
    void AddForce()
    {
        body.AddForce(Vector2.up * speed);
        Debug.Log("Perform operation");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        effect.transform.localScale *= transform.localScale.x;
        Destroy(gameObject);
    }
}
