using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _rayCastDitsnce;
    [SerializeField] private float fireRate;
    [SerializeField] private float canFire;
    [SerializeField] private int randomTarget;
    float xCurrentPostion;
    //------------------------------------
    [SerializeField] Transform[] target;
    [SerializeField] Transform rightRayCastOrgin;
    [SerializeField] Transform leftRayCastOrgin;
    [SerializeField] Transform fireFlakes;
    [SerializeField] Transform gun;  // point to insitate any item to hit the target (Player)
    [SerializeField] LayerMask layermask;

    //------------------------------------
    Rigidbody2D rb;
    Animator anim;
    RaycastHit2D hitRight;
    RaycastHit2D hitLeft;
    //-----------------------------------
    [SerializeField] bool Patrol;
    [SerializeField] bool attackPlayer;
    //-----------------------------------
   [SerializeField] GameObject attackParticle;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        randomTarget = Random.Range(0, target.Length);
        xCurrentPostion = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FlipSprite();
        AttackPlayer();
        xCurrentPostion = transform.position.x;
        CheckDistanceToPatrolPoints();
        FoundPlayer();
        Debug.DrawRay(rightRayCastOrgin.transform.position, Vector2.right, Color.red);
        Debug.DrawRay(leftRayCastOrgin.transform.position, -Vector2.right, Color.red);
    }
    private void FixedUpdate()
    {
        hitRight = Physics2D.Raycast(rightRayCastOrgin.transform.position, Vector2.right, _rayCastDitsnce, layermask);
        hitLeft = Physics2D.Raycast(rightRayCastOrgin.transform.position, -Vector2.right, _rayCastDitsnce, layermask);
        MoveTowrdPatrolPoints();
    }

    void CheckDistanceToPatrolPoints()
    {
        if (Vector2.Distance(transform.position, target[randomTarget].transform.position)> _stopDistance)
        {
            Patrol = true;
        }
       else if (Vector2.Distance(transform.position, target[randomTarget].transform.position) <= _stopDistance)
        {
            randomTarget = Random.Range(0, target.Length);
        }
    }

    void MoveTowrdPatrolPoints()
    {
        if(Patrol)
        {

            rb.MovePosition(Vector2.MoveTowards(transform.position, target[randomTarget].position, _speed*Time.deltaTime));
        }
    }
    void FlipSprite()
    {
        if (transform.position.x < xCurrentPostion&&Patrol)
        {
            transform.localScale = new Vector3(.2f, .2f, Time.deltaTime * 10);

        }

        else if (transform.position.x > xCurrentPostion && Patrol)
        {
            transform.localScale = new Vector3(-.2f, .2f, Time.deltaTime * 10);

        }
    }

    //------Finding and Attacking Player STATE-------------------------------

    void FoundPlayer()
    {
        if(hitRight.collider)
        {
            attackParticle.SetActive(true);
            attackPlayer = true;
            Patrol = false;
            transform.localScale = new Vector3(-.2f, .2f, Time.deltaTime * 10);
            

        }
        else if(hitLeft.collider)
        {
            attackParticle.SetActive(true);
            attackPlayer = true;
            Patrol = false;
            transform.localScale = new Vector3(.2f, .2f, Time.deltaTime * 10);
        }
       else if(hitLeft.collider==null||hitRight.collider==null)
        {
            attackPlayer = false;
            anim.SetTrigger("Idle");
            attackParticle.SetActive(false);
        }
    }
    
    void AttackPlayer()
    {
        if (attackPlayer)
        {
            anim.SetTrigger("attack");

        }
        // delay the rate of instaited items  of enemy weapon to attack Player
        if (attackPlayer&&Time.time> canFire)
        {
            fireRate = Random.Range(.5f, 1.5f);
            canFire = Time.time + fireRate;
            Instantiate(fireFlakes, gun.transform.position, Quaternion.identity);
        }
       
      
    }

    
}
