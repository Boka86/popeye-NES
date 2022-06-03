using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float _rayCastDitsnce;
    private float horizon;
    private float vertical;
    //-------------------------------------
    Vector2 movement;
    Rigidbody2D rb;
    private Animator anim;
    //-------------------------------------
    RaycastHit2D hit;
    [SerializeField] LayerMask layermask;
    [SerializeField] Transform JumprayCastOrgin;
    //-------------------------------------
    [SerializeField] bool canJump;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        EarroLogCheck();
    }

  
    void Update()
    {
        MovmentVariable();
        FlipPlayer();
        JumpingDetect();
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
        if(hit.collider)
        {
       
        }
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
}
