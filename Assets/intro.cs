using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class intro : MonoBehaviour {
    UnityEngine.UI.Text txtDialog;
    float timeStart;
    float direction = 1f;
    bool runFast = false;
    bool camFollow = false;
    bool shrinkIt = false;
    bool charFall = false;
    SpriteRenderer sprite;
    Rigidbody2D body;
    Animator anim, shrink;
    Camera cam;
	// Use this for initialization
	void Start () {
        GameObject text = GameObject.FindGameObjectWithTag("dialog");
        txtDialog = text.GetComponent<UnityEngine.UI.Text>();
        anim = GetComponent<Animator>();
        shrink = GameObject.FindGameObjectWithTag("shrinky").GetComponent<Animator>();
        anim.speed = .3f;
        timeStart = Time.time;
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(2);
        }
        int timeNow = (int)(Time.time - timeStart);
        Debug.Log(timeNow);
        if (anim.GetFloat("Direction") == 1f)
        {
            sprite.transform.Translate(.005f * ((direction == 1f)? (!runFast)?1 : 4: -4), 0f, 0f);
        }
        if (shrink.GetBool("ShrinkAnim") && timeNow < 54)
        {
            shrink.speed += .05f;
        }
        if (shrinkIt)
        {
            Vector3 scale = sprite.transform.localScale;
            scale.x -= .01f;
            scale.y -= .01f;
            if (scale.x < .1f)
            {
                shrinkIt = false;
                Debug.Log(scale);
            }
            else
            {
                sprite.transform.localScale = scale;
                Vector3 pos = Vector3.zero;
                pos.y += 0.0015f;
                pos.x -= 0.0015f;
                sprite.transform.Translate(pos);
            }
        }
        if (camFollow)
        {
            cam.transform.Translate(new Vector3(.005f * ((direction == 1f) ? (!runFast) ? 1 : 4 : -4), 0f, 0f));
        }
        if (charFall && timeNow < 60)
        {
            Vector3 pos = Vector3.zero;
            pos.y  -= 0.0015f;
            sprite.transform.Translate(pos);
        }
        switch (timeNow)
        {
            case 3:
                {
                    anim.SetFloat("Direction", 1f);
                    txtDialog.text = "You feel the sudden need to get closer to it..";
                    break;
                }
            case 6:
                {
                    txtDialog.text = "As you approch, the feeling of energy flowing in the room startles you.";
                    break;
                }
            case 9://56
                {
                    txtDialog.text = "Your parents haven't come home yet, in your despair... ";
                    break;
                } //54 crap hits the floor
            case 12:
                {
                    txtDialog.text = "..you had no choice but to find your father's work phone... ";
                    break;
                }
            case 14:
                {
                    anim.SetFloat("Direction", 0f);
                    break;
                }
            case 17:
                {
                    sprite.flipX = true;
                    break;
                }
            case 18:
                {
                    sprite.flipX = false;
                    break;
                }
            case 19:
                {
                    sprite.flipX = true;
                    break;
                }
            case 20:
                {
                    txtDialog.text = "You could not find it anywhere!";
                    break;
                }
            case 23:
                {
                    txtDialog.text = "*You run around frantically*";
                    direction = -1f;
                    anim.speed = 1.2f;
                    anim.SetFloat("Direction", 1f);
                    runFast = true;
                    break;
                }
            case 25:
                {
                    sprite.flipX = false;
                    direction = 1f;
                    break;
                }
            case 27:
                {
                    sprite.flipX = true;
                    direction = -1f;
                    break;
                }
            case 29:
                {
                    sprite.flipX = false;
                    direction = 1f;
                    break;
                }
            case 31:
                {
                    camFollow = true;
                    break;
                }
            case 33:
                {
                    camFollow = false;
                    anim.SetFloat("Direction", 0f);
                    anim.speed = .3f;
                    runFast = false;
                    break;
                }
            case 35:
                {
                    txtDialog.text = "Ouch! You ran your toe right into this machine!";
                    break;
                }
            case 38:
                {
                    txtDialog.text = "You wonder what this is for and why it holds a full grown person...";
                    sprite.flipX = true;
                    break;
                }
            case 42:
                {
                    txtDialog.text = "As you look around you see no hint of that work phone anywhere";
                    break;
                }
            case 45:
                {
                    txtDialog.text = "Your eyes peel over at the button on the machine, it might be in there....";
                    break;
                }
            case 46:
                {
                    anim.SetBool("Throwing", true);
                    body.transform.Translate(new Vector3(0f, .05f, 0f));
                    break;
                }
            case 47:
                {
                    anim.SetBool("Throwing", false);
                    shrink.SetBool("ShrinkAnim", true);
                    break;
                }
            case 54: //it hits the fan!
                {
                    txtDialog.text = "Oh no!!!!";
                    anim.SetFloat("Jumping", 1f);
                    shrinkIt = true;
                    body.isKinematic = true;
                    break;
                }
            case 57:
                {
                    shrink.SetBool("ShrinkAnim", false);
                    charFall = true;
                    break;
                }
            case 61:
                {
                    anim.SetBool("Throwing", false);
                    txtDialog.text = "'What is this place' you think to yourself";
                    break;
                }
            case 63:
                {
                    txtDialog.text = "You peak at the huge steps of the now gigantic machine and see 5 toothpicks.";
                    break;
                }
            case 66:
                {
                    txtDialog.text = "You pick them up, too heavy! If only somehow you can throw them.";
                    break;
                }
            case 69:
                {
                    txtDialog.text = "Ants are coming your way!!";
                    break;
                }
            case 73:
                {
                    SceneManager.LoadScene(2);
                    break;
                }
        }
	}
}
