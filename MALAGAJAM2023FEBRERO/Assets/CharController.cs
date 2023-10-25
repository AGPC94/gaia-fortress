using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [Header("Character")]
    //[SerializeField] protected Character character;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private float recoursePoints;
    [Space]
    [SerializeField] private float timeDamage;


    //Componentes
    protected Rigidbody2D rb;

    //Prooperties
    public float Health { get => health; set => health = value; }
    public float RecoursePoints { get => recoursePoints; set => recoursePoints = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public float Speed { get => speed; set => speed = value; }
    public float TimeDamage { get => timeDamage; set => timeDamage = value; }

    // Start is called before the first frame update
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        /*
        health = character.health;
        recoursePoints = character.recoursePoints;
        damage = character.damage;
        range = character.range;
        speed = character.speed;
        timeDamage = character.timeDamage;
        */

    }

    // Update is called once per frame
    public virtual void Update()
    {
        Die();
    }

    void Die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
