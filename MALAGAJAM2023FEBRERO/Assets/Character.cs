using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public float health;
    public float recoursePoints;
    public float damage;
    public float range;
    public float speed;
    public float timeDamage;
}
