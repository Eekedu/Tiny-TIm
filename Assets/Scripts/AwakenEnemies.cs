using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenEnemies : MonoBehaviour {

    GameController mGC;
	// Use this for initialization
	void Start () {
        mGC = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { mGC.AwakenEnemies(); }
    }
}
