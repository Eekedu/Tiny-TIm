using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMove : MonoBehaviour {
    Rigidbody2D self;
    // Use this for initialization
    void Start () {
        self = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject parent = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D rigidbody2D = parent.GetComponent<Rigidbody2D>();
        Debug.Log(rigidbody2D.position.x.ToString() + " is where I is");
        self.transform.position = new Vector3(rigidbody2D.transform.position.x /1.2f, 9f, 10);
	}
}
