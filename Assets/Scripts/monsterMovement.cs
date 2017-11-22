using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMovement : MonoBehaviour {

    BoxCollider2D body;
    Animator animator;
    float monsterSpeed = 1;
    float monsterYChange = -0.1f;
    float direction = 1;
    bool alive = true;
    float deadTimer = 0f;
    Collision2D oLast;
    SpriteRenderer spriteR;
    GameController mGC;

    private Enemy oMyHealth;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (oLast == collision) return;
        
            /*
            if (!collision.gameObject.tag.Equals("toothpick"))
            {
                Debug.Log("HERE" + collision.gameObject);
                oLast = collision;
                direction = - direction;
                Debug.Log("Got here hoss");
            }
            */
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("ground"))
        {
            oLast = null;
        }
    }

    // Use this for initialization
    void Start()
    {
        mGC = GameObject.FindObjectOfType<GameController>();
        oMyHealth = GetComponent<Enemy>();
        body = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        fLastX = body.transform.position.x;
    }

    // Update is called once per frame

    float fLastX;
	void Update () {
        if (!mGC.EnemiesAwake()) return;
        /* float monsterYChangeChance = Random.Range(1, 5000);
         monsterYChange = (monsterYChangeChance < 500) ? -0.1f : (monsterYChangeChance > 4500) ? 0.1f : 0f;*/
        float fMoved = body.transform.position.x-fLastX;
        fLastX = body.transform.position.x;

        
        if ((fMoved > 0.01f && direction > 0.0f)|| (fMoved < -0.01f && direction < 0.0f)) { direction = -direction; }
        //if ((fMoved>0.0001f && direction<0.0f) || (fMoved<0.0001f && direction>0)) { Debug.Log("damn" + fMoved.ToString()); direction = -direction; }
        if (!oMyHealth.AmIDead())
        {
            animator.SetBool("Walking", (direction != 0));
            body.transform.Translate(new Vector3(-(monsterSpeed * direction) / 10, 0, 0));
            spriteR.flipX = (direction < 0 && direction!=0);
        } else
        {
            animator.SetBool("Dead", true);
            deadTimer++;
            
            if (deadTimer > 100) { Destroy(this.gameObject); }
        }
	}
}
