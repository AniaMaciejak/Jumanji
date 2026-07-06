using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    protected GameObject player;
    protected Animator anim;
    protected Rigidbody rb;
    protected float distance;
    protected float timer;
    bool dead = false;

    public virtual void Move()
    {
    }
    public virtual void Attack()
    {
    }
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (!dead)
        {
            Attack();
        }
    }
    private void FixedUpdate()
    {
        if (!dead)
        {
            Move();
        }
    }
    public void ChangeHealth(int count)
    {
        // odejmowanie zdrowia
        health -= count;
        // jeœli zdrowie spada do zera lub ni¿ej, to...
        if (health <= 0)
        {
            // zmiana wartoœci zmiennej dead, co oznacza, ¿e wywo³ania funkcji Attack i Move przestan¹ dzia³aæ
            dead = true;
            // wy³¹czanie collidera wroga
            GetComponent<Collider>().enabled = false;
            // w³¹czanie animacji œmierci
            anim.SetBool("Die", true);
        }
    }
}

