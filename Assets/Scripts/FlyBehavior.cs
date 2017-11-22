using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehavior : MonoBehaviour {

    float fSwoopSpeed = 20.0f;
    float fFlySpeed = 15.0f;

    public float fMaxSideToSide = 50.0f;
    public float fYLevel = 10.0f;

    private float fMaxHealth;

    GameController m_myGC;
    Rigidbody2D m_myRigidBody;
    UnityEngine.UI.Image m_mHealthBar;
    Enemy m_myHealth;
	// Use this for initialization
	void Start () {
        m_myGC = FindObjectOfType<GameController>();
        m_myRigidBody = GetComponent<Rigidbody2D>();
        m_iState = -1;
        m_mHealthBar = GetComponentInChildren<UnityEngine.UI.Image>();
        m_myHealth = GetComponent<Enemy>();
        fMaxHealth = m_myHealth.fHealth;
	}
	
    private int m_iState;
	// Update is called once per frame
	void Update () {
        float fHealthScale = m_myHealth.fHealth / fMaxHealth;
        if (m_myHealth.AmIDead())
        {
            m_myGC.WinLevel(5.0f,4);
            Destroy(this.gameObject);
        }

        m_mHealthBar.rectTransform.localScale = new Vector3(fHealthScale,0.05f,1f);
        switch (m_iState)
        {
            case -1: PickNewState(); break;
            case 1: UpdateSwoop(); break;
            case 2: UpdateReloc(); break;
        }
        Debug.Log("State: " + m_iState.ToString());
	}

    void PickNewState()
    {
        switch (Random.Range(1, 3)) {
            case 1:        StartSwoop(); break;
            case 2: StartReloc(); break;
    }
    }

    Vector2 vPhaseStart;

    Vector2 vSwoopLocation;
    bool bSwooping;
    bool bReturning;

    Vector2 vRelocLocation;
    void StartReloc()
    {
        vRelocLocation = new Vector2(Mathf.Sin(Time.fixedTime) * fMaxSideToSide,fYLevel);
        m_iState = 2;
    }

    void UpdateReloc()
    {
        Vector2 l_destination = vRelocLocation;
        Vector2 l_vPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 l_Velocity = l_destination - (l_vPosition);
        l_Velocity.Normalize();
        l_Velocity *= (fFlySpeed * Time.deltaTime);
        //m_myRigidBody.velocity = l_Velocity;
        transform.position += new Vector3(l_Velocity.x, l_Velocity.y);
        if ((l_vPosition - l_destination).magnitude < 0.7f)
        {
                m_iState = -1;
        }
    }

    void StartSwoop()
    {
        m_iState = 1;
        bReturning = false;
        vSwoopLocation = m_myGC.GetCurrentLocation();
        vPhaseStart = new Vector2(transform.position.x, transform.position.y);
    }
    void UpdateSwoop()
    {
        Vector2 l_destination = bReturning ? vPhaseStart : vSwoopLocation;
        Vector2 l_vPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 l_Velocity = l_destination - (l_vPosition);
        l_Velocity.Normalize();
        l_Velocity *= (fSwoopSpeed * Time.deltaTime);
        //m_myRigidBody.velocity = l_Velocity;
        transform.position += new Vector3(l_Velocity.x,l_Velocity.y);
        if ((l_vPosition - l_destination).magnitude < 0.7f)
        {
            if (!bReturning)
            {
                bReturning = true;
            } else
            {
                m_iState = -1;
            }
        }
    }
}
