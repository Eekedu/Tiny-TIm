using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
        		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    float fNextHit;
    private void CollideEvent(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player == null) return;
        if (Time.fixedTime < fNextHit) return;
        player.ApplyDamage(5);
        fNextHit = Time.fixedTime + 2.0f;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollideEvent(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollideEvent(collision);
    }

}
