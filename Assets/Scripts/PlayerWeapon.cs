using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public float fMyDamage;

    private bool bDamageApplied;
    private void Start()
    {
        bDamageApplied = false;
    }

    private void CollideEvent(Collider2D other)
    {
        Enemy baddie = other.GetComponent<Enemy>();
        if (baddie == null || bDamageApplied) return;
        bDamageApplied = true;
        baddie.ApplyDamage(fMyDamage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollideEvent(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollideEvent(collision.collider);
    }
}