using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : CharController
{
    [Header("Ally")]
    [SerializeField] float timeCooldown;
    [SerializeField] float rotSpeed;
    [SerializeField] Sprite portrait;

    BtnAlly btn;

    Coroutine currentCo;

    public Sprite Portrait { get => portrait; set => portrait = value; }
    public BtnAlly Btn { get => btn; set => btn = value; }
    public float TimeCooldown { get => timeCooldown; set => timeCooldown = value; }

    public override void Awake()
    {
        base.Awake(); 
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        RotateInDirection();
    }
    void RotateInDirection()
    {
        if (Speed != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
        }
    }

}



