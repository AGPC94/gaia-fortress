using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : Ally
{
    [Header("Shoot")]
    [SerializeField] float shootSpeed;
    [SerializeField] float shootCooldown;
    [SerializeField] bool shootingAllowed;
    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] GameObject projectile;

    [SerializeField] string sound;

    Collider2D nearestenemy;

    // Start is called before the first frame update


    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        Shot();
    }

    public void Shot()
    {
        Collider2D[] enemiesAround = Physics2D.OverlapCircleAll(gameObject.transform.position, Range, whatIsEnemy);
        float shortestDistance = Mathf.Infinity;

        if (shootingAllowed)
        {
            for (int i = 0; i < enemiesAround.Length; i++)
            {
                if (Vector3.Distance(transform.position, enemiesAround[i].transform.position) < shortestDistance && nearestenemy == null)
                {
                    nearestenemy = enemiesAround[i];
                    Vector2 direction = (nearestenemy.transform.position - transform.position).normalized;
                    Projectile clone = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                    clone.Damage = Damage;
                    clone.Move(direction, shootSpeed);
                    shootingAllowed = false;

                    AudioManager.instance.Play(sound);

                    StartCoroutine(Cooldown());

                    Debug.Log("Disparo enemigo: " + nearestenemy.name);
                }
                else
                    break;
            }
        }
        else
        {
            Debug.Log("No Disparo enemigo");
        }


    }


    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        nearestenemy = null;
        shootingAllowed = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
