using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UnityEngine.UI.Text zapdos = GetComponent<UnityEngine.UI.Text>();

        //zapdos.text = "haha";
        controller PL = GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
        zapdos.text = "Toothpicks: " + PL.GetAmmo().ToString();
	}
}
