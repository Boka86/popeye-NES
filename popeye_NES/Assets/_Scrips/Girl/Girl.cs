using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    Rigidbody2D rb;
    
    //-----------------------
    [SerializeField] private float throwDealy;
    [SerializeField] private float canThrow;
    //----------------------
    [SerializeField] Transform heartSprite;
    [SerializeField] Transform gun;

    //-----------------------
    [SerializeField] bool throwHeart;
    Player player;
    //-----------------------
    Animator anim;
   public  Flip flip;
    
    void Start()
    {
        flip = GetComponent<Flip>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
        canThrow = 2;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameOver==false)
        {
            ThrowHeart();
        }
        else if (player.gameOver == true)
        {
            anim.SetTrigger("tripOver");
            flip.enabled = false;
    
        }
        
    }


    void ThrowHeart()
    {
        if(canThrow<Time.time)
        {
            throwDealy = Random.Range(7f, 15f);
            canThrow = throwDealy + Time.time;
            Instantiate(heartSprite, gun.transform.position, Quaternion.identity);


        }
    }

    

}
