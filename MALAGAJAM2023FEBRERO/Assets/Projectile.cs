using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer renderer;
    [SerializeField] bool isPoison;
    float damage;

    public float Damage { get => damage; set => damage = value; }

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!renderer.isVisible)
            Destroy(gameObject);
    }

    public void Move(Vector2 direction, float speed)
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            enemy.Health -= damage;

            if (isPoison)
            {
                enemy.Poisoned();
            }
            Destroy(gameObject);
        }
    }
}
