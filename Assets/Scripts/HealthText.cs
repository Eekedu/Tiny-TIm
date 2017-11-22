using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthText : MonoBehaviour
{

    // Use this for initialization
    UnityEngine.UI.Text myText;
    GameController myGC;
    void Start()
    {
        myText = GetComponent<UnityEngine.UI.Text>();
        myGC = GameController.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = "Health: " + myGC.GetCurrentHealth().ToString();
    }
}
