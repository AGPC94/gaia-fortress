using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Ally
{
    [Header("Attack")]
    [SerializeField] float atkSpeed;
    [SerializeField] float atkTime;
    [SerializeField] float atkCooldown;
    [SerializeField] bool atkAllowed;
    [SerializeField] LayerMask whatIsEnemy;

    Animator anim;

    Collider2D nearestenemy;

    // Start is called before the first frame update


    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        Chase();
    }

    public void Chase()
    {
        Collider2D[] enemiesAround = Physics2D.OverlapCircleAll(gameObject.transform.position, Range, whatIsEnemy);
        float shortestDistance = Mathf.Infinity;

        if (atkAllowed)
        {
            for (int i = 0; i < enemiesAround.Length; i++)
            {
                if (Vector3.Distance(transform.position, enemiesAround[i].transform.position) < shortestDistance && nearestenemy == null)
                {
                    nearestenemy = enemiesAround[i];
                    Vector2 direction = (nearestenemy.transform.position - transform.position).normalized;

                    rb.velocity = direction * atkSpeed;

                    atkAllowed = false;

                    Debug.Log("Ataca a: " + nearestenemy.name);
                }
                else
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AudioManager.instance.Play("Ant");

            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Health -= Damage;

            anim.SetTrigger("explo");

            Destroy(gameObject, 0.50f);

            rb.velocity = Vector2.zero;

            //enabled = false;



        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
