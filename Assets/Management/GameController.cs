using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public controller m_PlayerFab;
    public GameObject m_PickFab;
    public float fPlayerXPos;
    public float fPlayerYPos;

    private controller m_oCurrentPlayer;
    private int m_iLives;
	// Use this for initialization
	void Start () {
        bSpawning = true;
        fSpawnTime = Time.fixedTime + 2.0f;
        m_iLives = 3;
        vBackaways2 = new Vector2(fPlayerXPos, fPlayerYPos);
	}

    private float fTimeMachine;
    Vector2 vBackaways;
    Vector2 vBackaways2;
    
    // Update is called once per frame
	void Update () {
	    if (bSpawning)
        {
            if (Time.fixedTime > fSpawnTime)
            {
                ExecuteSpawn();
            }
        }
        if (bAlive)
        {
            if (Time.fixedTime > fTimeMachine)
            {
                ExecTimeMachine();
            }
        }
        if (bWinning)
        {
            if (Time.fixedTime >= fWinTime)
            {
                ExecuteWin();
            }
        }
	}

    private float fSpawnTime;
    private bool bSpawning;
    private bool bAlive;



    public void StartGame()
    {

    }

    public void KillPlayer()
    {
        Debug.Log("Got Here Somehow");
        bAlive = false;
        bSpawning = true;
        Destroy(m_oCurrentPlayer.gameObject);
        fSpawnTime = Time.fixedTime + 1.0f;
        m_oCurrentPlayer = null;
        m_iLives--;
    }

    bool bWakeup=false;
    public bool EnemiesAwake()
    {
        return bWakeup;
    }
    public void AwakenEnemies()
    {
        bWakeup = true;
    }

    private void ExecuteSpawn()
    {
        //Debug.Log("Spawnd" + bSpawning.ToString() + );
        bSpawning = false;
        if (m_iLives<=0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(6);
        }
        bAlive = true;
        m_oCurrentPlayer = GameObject.Instantiate(m_PlayerFab, new Vector3(vBackaways2.x,vBackaways2.y), Quaternion.identity);
        m_oCurrentPlayer.Weapon1 = m_PickFab;
    }
    private void ExecTimeMachine()
    {
        vBackaways2 = vBackaways;
        vBackaways = new Vector2(m_oCurrentPlayer.transform.position.x, m_oCurrentPlayer.transform.position.y);
        fTimeMachine = Time.fixedTime + 3.0f;
    }

    public int GetCurrentPicks()
    {
        if (m_oCurrentPlayer == null) return 0;
        return m_oCurrentPlayer.GetAmmo();
    }
    public float GetCurrentHealth()
    {
        if (m_oCurrentPlayer == null) return 0.0f;
        return m_oCurrentPlayer.GetComponent<Damageable>().fHealth;
    }
    public int GetCurrentLives()
    {
        return m_iLives; ;
    }
    public Vector2 GetCurrentLocation()
    {
        if (m_oCurrentPlayer == null) return vBackaways2;
        return new Vector2(m_oCurrentPlayer.transform.position.x, m_oCurrentPlayer.transform.position.y);
    }

    private bool bWinning;
    private int iNextScene;
    private float fWinTime;
    public void WinLevel(float fLingerTime, int nextscene)
    {
        bWinning = true;
        iNextScene = nextscene;
        fWinTime = Time.fixedTime + fLingerTime;
    }
    public void ExecuteWin()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(iNextScene);
    }
}
