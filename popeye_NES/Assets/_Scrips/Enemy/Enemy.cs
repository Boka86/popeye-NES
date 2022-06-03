using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    [SerializeField] private int randomTarget;
    float xCurrentPostion;
    //------------------------------------
    [SerializeField] Transform[] target;
    Vector2 follow;
   
    //------------------------------------
    Rigidbody2D rb;
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
    }

    private void FixedUpdate()
    {
        FollowPatrolPoints();
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

    void FollowPatrolPoints()
    {
        if(Patrol)
        {

            rb.MovePosition(Vector2.MoveTowards(transform.position, target[randomTarget].position, _speed*Time.deltaTime));
        }
    }
    void FlipSprite()
    {
        if (transform.position.x < xCurrentPostion)
        {
            transform.localScale = new Vector3(.2f, .2f, Time.deltaTime * 10);

        }

        else if (transform.position.x > xCurrentPostion)
        {
            transform.localScale = new Vector3(-.2f, .2f, Time.deltaTime * 10);

        }
    }
}
