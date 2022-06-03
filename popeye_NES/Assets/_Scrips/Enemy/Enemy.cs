using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _stopDistanceFromPlayer;
    [SerializeField] private float _rayCastDitsnce;
    [SerializeField] private int randomTarget;
    float xCurrentPostion;
    //------------------------------------
    [SerializeField] Transform[] target;
    [SerializeField] Transform rightRayCastOrgin;
    [SerializeField] Transform leftRayCastOrgin;
    [SerializeField] Transform snowFlakes;
    [SerializeField] LayerMask layermask;

    //------------------------------------
    Rigidbody2D rb;
    RaycastHit2D hitRight;
    RaycastHit2D hitLeft;
    //-----------------------------------
    [SerializeField] bool Patrol;
    void Start()
    {
        randomTarget = Random.Range(0, target.Length);
        xCurrentPostion = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FlipSprite();
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
            Patrol = false;
            transform.localScale = new Vector3(-.2f, .2f, Time.deltaTime * 10);
            AttackPlayer();



        }
        else if(hitLeft.collider)
        {
            Patrol = false;
            transform.localScale = new Vector3(.2f, .2f, Time.deltaTime * 10);
            AttackPlayer();
        }
    }
    
    void AttackPlayer()
    {
        for(int i=0;i>4;i++)
        {
            Instantiate(snowFlakes, transform.position, Quaternion.identity);
            i++;
            Debug.Log(i);
           
        }
    }
}
