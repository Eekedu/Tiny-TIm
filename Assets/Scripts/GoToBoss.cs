using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBoss : MonoBehaviour {

    // Use this for initialization
    GameController mGC;
	void Start () {
        mGC = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") mGC.WinLevel(1.0f, 3);
    }
}
