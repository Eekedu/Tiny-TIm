using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {


    public float fHealth;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyDamage(float fDamage)
    {

        fHealth = fHealth - fDamage;
        Damaged();
        Debug.Log("I have " + fHealth.ToString());
    }

    protected virtual void Damaged()
    {
    }

    public bool AmIDead()
    {
        return fHealth <= 0.0f;
    }
}
