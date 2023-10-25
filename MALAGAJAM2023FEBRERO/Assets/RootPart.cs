using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootPart : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float number;
    [SerializeField] float maxHealth;
    [SerializeField] float health;

    [Header("Suck")]
    [SerializeField] bool isSucking;
    [SerializeField] float suckTime;
    [SerializeField] float healthRecover;

    [Header("Sprites")]
    [SerializeField] Sprite root;
    [SerializeField] Sprite rootPoint;

    SpriteRenderer renderer;

    Root rootParent;
    public float Number { get => number; set => number = value; }
    public float Health { get => health; set => health = value; }

    // Start is called before the first frame update
    void Awake()
    {
        rootParent = transform.GetComponentInParent<Root>();
        renderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    // Update is called once per frame
     void Update()
    {
        if (number == rootParent.Progress)
        {
            renderer.sprite = rootPoint;
        }
        else
            renderer.sprite = root;
    }

    public void Hurt(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            rootParent.Progress = number - 1;
            health = maxHealth;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water") && !isSucking)
        {
            StartCoroutine(Suck());
        }
    }

    IEnumerator Suck()
    {
        Debug.Log("Chupa agua");
        isSucking = true;
        yield return new WaitForSeconds(suckTime);
        GameManager.instance.treeProgress += healthRecover;
        isSucking = false;
    }

}
