using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharController
{
    [Header("Enemy")]
    [SerializeField] float rotSpeed;
    [SerializeField] float timeStart;
    [SerializeField] float timeSpawn;
    [SerializeField] float stopDistance;
    [SerializeField] bool isAttacking;
    [SerializeField] GameObject target;

    [Header("Poison")]
    [SerializeField] bool isPoisoned;
    [SerializeField] float poisonTime;
    const float POISONDAMAGE = 20;
    [SerializeField] GameObject closestRoot;
    [SerializeField] GameObject rootMother;

    [Header("Tank")]
    [SerializeField] bool tank;
    Animator animator;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public float TimeStart { get => timeStart; set => timeStart = value; }
    public float TimeSpawn { get => timeSpawn; set => timeSpawn = value; }
    public bool Tank { get => tank; set => tank = value; }

    //public bool IsPoisoned { get => isPoisoned; set => isPoisoned = value; }


    public override void Awake()
    {
        base.Awake();
        rootMother = GameObject.Find("RootMother");
        animator = GetComponent<Animator>();
        Debug.Log(rootMother.name);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        RotateToTarget();

        animator.SetBool("isAttacking", isAttacking);
    }

    void FixedUpdate()
    {
        GameObject[] roots = GameObject.FindGameObjectsWithTag("Root");
        closestRoot = GetClosestGO(roots);

        if (!isAttacking)
        {
            //Raíz más cerca
            if (closestRoot != null)
            {
                ChaseTarget(closestRoot);
            }
            //Núcleo
            else
            {
                ChaseTarget(rootMother);
            }
        }
        else
            ChaseTarget(target);

    }

    void RotateToTarget()
    {
        if (rb.velocity != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
        }
    }

    void ChaseTarget(GameObject target)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;

        if (Vector2.Distance(target.transform.position, transform.position) <= stopDistance)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = direction * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AllyTrigger"))
        {
            isAttacking = true;
            target = collision.transform.parent.gameObject;
        }

        if (collision.CompareTag("RootMother"))
        {
            isAttacking = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AllyTrigger"))
        {
            isAttacking = false;
            target = null;
        }
        if (collision.CompareTag("RootMother"))
        {
            isAttacking = false;
        }
    }

    public void Poisoned()
    {
        if (!isPoisoned)
        {
            isPoisoned = true;
            StartCoroutine(PoisonCo());
        }
    }

    IEnumerator PoisonCo()
    {
        while (isPoisoned)
        {
            yield return new WaitForSeconds(poisonTime);
            Health -= POISONDAMAGE;
            Debug.Log("Enemigo " + name + " esta envenenado.");
        }
    }

    Transform GetClosestTr(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    GameObject GetClosestGO(GameObject[] gos)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in gos)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    
    void OnDestroy()
    {
        GameManager.instance.recoursePoints += RecoursePoints;
    }
    
}
