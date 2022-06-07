using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float _rayCastDitsnce;
    [SerializeField] private int liveCount;
    private int heartCount;
    private float horizon;
    private float vertical;
    //-------------------------------------
    Vector2 movement;
    Rigidbody2D rb;
    private Animator anim;
    Lives lives;
    Hearts hearts;
    //-------------------------------------
    RaycastHit2D hit;
    [SerializeField] LayerMask layermask;
    [SerializeField] Transform JumprayCastOrgin;
    //-------------------------------------
    [SerializeField] bool canJump;
    public bool gameOver;

    //-----------------------
    AudioSource source;
    [SerializeField] AudioClip hitEffect;
    //----------------------
    GameObject[] enemyGO;
   


    void Start()
    {

        enemyGO= GameObject.FindGameObjectsWithTag("Enemy");
        
        lives = GameObject.Find("Canvas").GetComponent<Lives>();
        source = GetComponent<AudioSource>();
        
       

        if(lives==null)
        {
            Debug.LogError(" Cant find Canvas");
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        EarroLogCheck();
    }

  
    void Update()
    {
        if(gameOver!=true)
        {
            MovmentVariable();
            FlipPlayer();
            JumpingDetect();
            Dead();
        }
        QuitGame();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        Jump();
        hit = Physics2D.Raycast(JumprayCastOrgin.transform.position, Vector2.down, _rayCastDitsnce, layermask);
        Debug.DrawRay(JumprayCastOrgin.transform.position,Vector2.down,Color.blue);
    }
    void MovmentVariable()
    {
        movement = new Vector2(horizon * _speed, 0);
        horizon = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void PlayerMovement()
    {
        if(horizon>0.1f||horizon<-0.1f)
        {
            anim.SetBool("IsRunning", true);
            rb.AddForce(movement,ForceMode2D.Impulse);
        }

        else
        {
            anim.SetBool("IsRunning", false);
        }

    }
    void JumpingDetect()
    {
       
        if(hit.collider!=null&& Input.GetKeyDown(KeyCode.Space))
        {
            canJump = true;
            
        }
        else if(hit.collider==null)
        {
            canJump = false;
          
        }

        
    }
    void Jump()
    {
        if(canJump)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        
    }
    void FlipPlayer()
    {
        if(horizon>0.1f)
        {
            transform.localScale = new Vector2(1,1);
        }
        else if(horizon < -0.1f)

        {
            transform.localScale = new Vector2(-1, 1);
        }
    }


    void EarroLogCheck()
    {
        if(anim==null)
        {
            Debug.LogError("Cant Find Animator");
        }
    }
    //------------------------Lives check------------------
    public void PlayerLives()
    {
        liveCount--;
        lives.UpdateLives(liveCount);
        if(source.isPlaying==false)
        {
            
            source.PlayOneShot(hitEffect);
        }
    }
    public void HeartCount()
    {
        heartCount++;

        lives.UpdateHeartCollected(heartCount);
        if(heartCount>=16)
        {
            Debug.Log(" Level Done ");
            StartCoroutine(NextLevel());
        }
    }

    void Dead()
    {
        if(liveCount<=0)
        {
            gameOver=true;
            rb.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            liveCount = 0;
            anim.SetBool("IsDead", true);
            StartCoroutine(RestartLevel());
        }
    }

    
    
   
    
    public void GameOver()
    {
        gameOver = true;
        anim.SetBool("IsDead", true);
        rb.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        StartCoroutine(RestartLevel());
        if (gameOver==false)
        {
            MovmentVariable();
            FlipPlayer();
            JumpingDetect();
            Dead();
        }
        
    }

    public IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    public IEnumerator NextLevel()
    {
        foreach (GameObject enemyGO in enemyGO)
        {
            Destroy(enemyGO);

        }
         yield return new WaitForSeconds(4f);
         SceneManager.LoadScene(0);

    }

    void QuitGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
   
}
