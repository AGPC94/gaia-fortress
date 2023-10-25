using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxEnemy : MonoBehaviour
{
    [SerializeField] float timeDamage;
    Enemy enemy;

    Coroutine currentCo;
    [SerializeField] bool isAttacking;

    //[SerializeField] bool isCoroutineExecuting;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttacking)
        {
            if (collision.CompareTag("Ally"))
            {
                Ally Ally = collision.GetComponent<Ally>();
                currentCo = StartCoroutine(AllyDamage(enemy.Damage, enemy.TimeDamage, Ally));
            }

            if (collision.CompareTag("Root"))
            {
                RootPart rp = collision.GetComponent<RootPart>();
                currentCo = StartCoroutine(RootPartDamage(enemy.Damage, enemy.TimeDamage, rp));
            }

            if (collision.CompareTag("RootMother"))
            {
                currentCo = StartCoroutine(RootMotherDamage(enemy.Damage, enemy.TimeDamage));
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Ally") || collision.CompareTag("Root"))
        {
            isAttacking = false;
            StopAllCoroutines();
        }
        */
    }

    IEnumerator AllyDamage(float dmg, float timeDmg, Ally ally)
    {
        isAttacking = true;
        ally.Health -= dmg;
        yield return new WaitForSeconds(timeDmg);
        isAttacking = false;
        Debug.Log("Daña al aliado");
    }
    IEnumerator RootPartDamage(float dmg, float timeDmg, RootPart rp)
    {
        isAttacking = true;
        rp.Hurt(dmg);
        yield return new WaitForSeconds(timeDmg);
        isAttacking = false;
        Debug.Log("Daña a la raiz");
    }
    IEnumerator RootMotherDamage(float dmg, float timeDmg)
    {
        isAttacking = true;
        GameManager.instance.Health -= dmg;
        yield return new WaitForSeconds(timeDmg);
        isAttacking = false;
        Debug.Log("Daña a la madre tierra");
    }


}