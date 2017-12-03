using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour {

    public GameObject Weapon1;

    public static GameObject currentWeapon;
	//Matrix4x4/ Use this for initialization
	private Transform _PlayerPosition;
	public float jumpSpeed = 600f;
    public float velocityDivide = 10f;
    public float throwStrength = 2f;
    private Rigidbody2D body;
    public static AudioSource aSource;
    private SpriteRenderer sprite;
    private Animator animator;
    GameObject[] floors;
    //HealthBar health = new HealthBar();
    float size = 0;
    bool canJump = false;
    public static float scale;
    public static float scaleBig = 10f, scaleSmall = 2.5f, scaleNormal = 5f;
    bool isChangingSize = false, scaleToNormal = false;
    private bool faceLeft = false;
    private int m_iAmmo;

	void Start () {       
		body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        floors = GameObject.FindGameObjectsWithTag("ground");
        aSource = GetComponent<AudioSource>();
        currentWeapon = Weapon1;
        m_iAmmo = 5;
    }

    Collider2D oLast;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("ground")
            || other.tag.Equals("toothpick"))
        {
            oLast = other;
            bGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other == oLast) return;
        LotsaPix LPix = other.GetComponent<LotsaPix>();
        if (LPix != null) m_iAmmo = 5;
        if (other.tag.Equals("ground")
            || other.tag.Equals("toothpick"))
        {
            oLast = other;
            bGrounded = true;
        }
    }

    private bool CanIJump()
    {
        return (bGrounded && (body.velocity.y < 0.1f));
    }

    private float GetJumpImpulse()
    {
        Debug.Log(scale);
        return 1.5f * scale;
    }


    private bool bGrounded;
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("ground") || other.tag.Equals("toothpick"))
        {
            oLast = null;
            bGrounded = false;
        }
    }

    private void TriggerWeapon(Vector2 vPosition, Vector2 vDirection)
    {
        if (m_iAmmo <= 0) return;
        GameObject currentWeaponNew = Instantiate(currentWeapon, body.position, new Quaternion(0, 0, 0, 0));
        Rigidbody2D pickBody = currentWeaponNew.GetComponent<Rigidbody2D>();
        pickBody.transform.position = new Vector3(vPosition.x, vPosition.y);
        Vector2 to = new Vector2(pickBody.transform.position.x, pickBody.transform.position.y);
        pickBody.transform.Rotate(Vector3.back, vDirection.y > 0.0f ? Vector2.Angle(-vDirection, Vector3.right) : -Vector2.Angle(-vDirection, Vector3.right));
        //pickBody.rigidbody2D.
        pickBody.velocity = vDirection * 10.0f;
        m_iAmmo--;
    }

    // Update is called once per frame
    void Update () {
        float vert;
        //if (!Input.GetMouseButton(0)) { vert = Input.GetAxis("Horizontal"); } else { vert = 0; } 
        vert = Input.GetAxis("Horizontal");
        animator.SetFloat("Direction", (vert != 0) ? 1 : 0);
        faceLeft = (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ? true : (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) ? false: faceLeft;
        sprite.flipX = faceLeft;
        scale = sprite.transform.localScale.x;
        if (Input.GetKey(KeyCode.Keypad1))
        {
            //currentWeapon = GameObject.FindGameObjectWithTag("toothpick");
            currentWeapon = Weapon1;
        }
        GameObject arrow = GameObject.FindGameObjectWithTag("arrow");
        if ((currentWeapon!=null) && (arrow!=null))
        {
            SpriteRenderer render = arrow.GetComponent<SpriteRenderer>();
            float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            Vector2 mPos = new Vector2(mouseX, mouseY);

            //float arrowX = Mathf.Cos(mouseX) + body.position.x;
            Vector2 aDirection = mPos - body.position;
            aDirection.Normalize();
            aDirection *= 5.0f;
            float arrowX = body.position.x + aDirection.x;
            //float arrowY = Mathf.Sin(mouseY) + body.position.y;
            float arrowY = body.position.y + aDirection.y;
            if (Input.GetMouseButton(0))
            {
                animator.SetBool("Throwing", true);
                render.enabled = true;


                arrow.transform.position = new Vector2(arrowX, arrowY);
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("Throwing", false);
                Debug.Log("Damn");
                TriggerWeapon(new Vector2(arrowX,arrowY),aDirection);
                render.enabled = false;

            }
        }
        if (Input.GetKeyDown(KeyCode.F) && !isChangingSize) {
            isChangingSize = true;
            size = (scale != scaleBig)? 0.1f: -0.1f;
            scaleToNormal = (scale == scaleBig);
        }
        if (Input.GetKeyDown(KeyCode.G) && !isChangingSize) {
            isChangingSize = true;
            size = (scale != scaleSmall) ? -0.1f : 0.1f;
            scaleToNormal = (scale == scaleSmall);
        }
        if (isChangingSize)
        {
            scale = scale + size;
            if (scale > scaleBig) { scale = scaleBig; isChangingSize = false; }
            if (scale < scaleSmall) { scale = scaleSmall; isChangingSize = false; }
            Debug.Log(scale);
            if (scaleToNormal)
            {
                if (scale > scaleNormal - .1 && scale < scaleNormal + .1)
                {
                    scale = scaleNormal;
                    scaleToNormal = false;
                    isChangingSize = false;
                }
            }
            sprite.transform.localScale = new Vector3(scale, scale);
        }
		bool isSpaceDown = Input.GetKey ("space");
        if (isSpaceDown && CanIJump())
        {
            ExecJump();
        }
        bool isYChanging = (body.velocity.y > .1f || body.velocity.y < -0.1f); 
        
        animator.SetFloat("Jumping", (isYChanging) ? 1 : 0);

        if (vert != 0)
        {
            body.transform.Translate(new Vector2(vert / velocityDivide, 0));
            velocityDivide -= 0.1f;
            velocityDivide = (velocityDivide < 5) ? 5 : velocityDivide;
        } else
        {
            velocityDivide += 0.05f;
            velocityDivide = (velocityDivide > 10) ? 10 : velocityDivide;
        }

        float upSpeed = body.velocity.y;
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Camera cam = camera.GetComponent<Camera>();
        cam.orthographicSize = scale;// + (scale / 2.5f);
        float diff = cam.orthographicSize - 7f;
        float fModifier = 0.1f;
        camera.transform.position = new Vector3(body.transform.position.x, body.transform.position.y + (15 * fModifier), -10);
        //camera.transform.position = new Vector3(body.transform.position.x, (scale / 2.5f) + (diff / 2), -10);
        //camera.transform.position = new Vector3(body.transform.position.x, body.transform.position.y + 5, -10);
    }
    public int GetAmmo()
    {
        return m_iAmmo;
    }
    private void ExecJump()
    {
        AudioClip jump = Resources.Load<AudioClip>((scale == scaleBig) ? "Jump_Big" : "Jump_Small");
        aSource.PlayOneShot(jump);
        //body.AddForce(new Vector2(0f, jumpSpeed * 15));
        body.velocity = new Vector2(body.velocity.x, body.velocity.y + GetJumpImpulse()) ;

    }
}
