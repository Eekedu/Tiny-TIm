using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damageable {

	// Use this for initialization
	void Start () {
		
	}

    protected override void Damaged()
    {
        if (fHealth <= 0.0f)
        {
            GameController gc = GameObject.FindObjectOfType<GameController>();
            gc.KillPlayer();
        }
    }
    // Update is called once per frame
    void Update () {
	}
}
