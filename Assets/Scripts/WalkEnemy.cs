using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : Enemy
{
    // Start is called before the first frame update
    // Prêdkoœæ chrz¹szcza
    [SerializeField] float speed;
    // Obszar wykrywania chrz¹szcza
    [SerializeField] float detectionDistance;
    float patrolTimer;
    public override void Move()
    {
        // Je¿eli odleg³oœæ miêdzy wrogiem a graczem jest mniejsza ni¿ promieñ wykrywania chrz¹szcza
        // ORAZ odleg³oœæ miêdzy wrogiem a graczem jest wiêksza ni¿ promieñ ataku, to:
        if (distance < detectionDistance && distance > attackDistance)
        {
            // Obrócenie wroga w stronê gracza
            transform.LookAt(player.transform);
            // W³¹czenie animacji biegu
            anim.SetBool("Run", true);
            // Poruszanie chrz¹szczem do przodu
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
        else if (distance > detectionDistance)
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
            patrolTimer += Time.deltaTime;
            anim.SetBool("Run", true);
            if (patrolTimer > 15)
            {
                transform.Rotate(new Vector3(0, 90, 0));
                patrolTimer = 0;
            }
        }
        // W przeciwnym razie:
        else
        {
            // Wy³¹czenie animacji biegu
            anim.SetBool("Run", false);
        }
    }
    public override void Attack()
    {
        // W³¹czenie timera
        timer += Time.deltaTime;
        // Je¿eli odleg³oœæ miêdzy wrogiem a graczem jest mniejsza ni¿ odleg³oœæ ataku i wartoœæ timera jest wiêksza ni¿ cooldown ataku
        if (distance < attackDistance && timer > cooldown)
        {
            // Resetowanie timera
            timer = 0;
            // Pobranie skryptu gracza i wywo³anie funkcji odejmowania zdrowia
            player.GetComponent<PlayerController>().ChangeHealth(damage);
            // W³¹czenie animacji ataku
            anim.SetBool("Attack", true);
        }
        // W przeciwnym razie...
        else
        {
            // Wy³¹czenie animacji ataku
            anim.SetBool("Attack", false);
        }
    }
}
