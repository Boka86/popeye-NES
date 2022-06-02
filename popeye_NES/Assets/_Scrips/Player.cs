using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float horizon;
    private float vertical;
    //-------------------------------------
    Vector2 movement;
    Rigidbody2D rb;
    private Animator anim;
    //-------------------------------------

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
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        
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
            rb.AddForce(movement, ForceMode2D.Impulse);
        }

        else
        {
            anim.SetBool("IsRunning", false);
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
