using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StartClick : MonoBehaviour {

	void Update () {
		if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        } else if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
