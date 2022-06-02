using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    Rigidbody2D rb;
    //-----------------------
    [SerializeField] private float _speed;
    [SerializeField] private int randomSpot;
    //-----------------------
    private Vector2 _movement;

    //-----------------------
    [SerializeField] Transform[] wayPoints;
    //-----------------------
 

    void Start()
    {
        randomSpot = Random.Range(0, wayPoints.Length);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2( 1* _speed,0);
        FollowWayPoints();
    }

    private void FixedUpdate()
    {
        
        Movement();
    }
    void FollowWayPoints()
    {


        _movement = Vector2.MoveTowards(transform.position, wayPoints[randomSpot].transform.position, _speed);



    }

    void Movement()
    {
        rb.AddForce(-_movement, ForceMode2D.Impulse);
    }
}
