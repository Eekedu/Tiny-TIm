using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothPick : MonoBehaviour
{
    // Use this for initialization
    float rotation = 0f;
    float speed = 0f;
    Rigidbody2D body;
    float fTTL;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other + "laded");
        if (other.tag.Equals("ground"))
        {
            AudioClip hit = Resources.Load<AudioClip>((controller.scale == controller.scaleBig) ? "Toothpick_Stick_Big" : "Toothpick_Stick_Small");
            controller.aSource.PlayOneShot(hit);
            body.velocity = Vector2.zero;
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("Sticked");
        }
    }

    void Start () {
        fTTL = Time.fixedTime + 7.0f;
        body = GetComponent<Rigidbody2D>();
        //body.AddForce(new Vector2(200f, 0));
    }

    public void setRotation(float rotation)
    {
        this.setRotation(rotation);
    }

	
	// Update is called once per frame
	void Update () {
        //Debug.Log("smd");
        if (Time.fixedTime > fTTL) Destroy(this.gameObject);
	}
}
