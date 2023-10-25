using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadillo : Ally
{
    [SerializeField] float destroy;

    const float posX = -10f;

    public override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * Speed;

        AudioManager.instance.Play("ArmadilloMove");
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }

    public override void Update()
    {
        base.Update();

        if (transform.position.x >= destroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AudioManager.instance.Play("ArmadilloHit");

            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy.Tank)
            {
                enemy.Health -= Damage;
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
