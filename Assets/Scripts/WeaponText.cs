using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponText : MonoBehaviour {
    UnityEngine.UI.Text text;
    // Use this for initialization
    void Start () {
         text = GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (controller.currentWeapon == null) return;
        text.text = "Weapon: " + controller.currentWeapon.ToString().ToUpperInvariant();
	}
}
