using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    // Start is called before the first frame update
    
    float xCurrentPostion;
    void Start()
    {
        xCurrentPostion = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        FlipSprite();
        xCurrentPostion = transform.position.x;
    }
    private void FixedUpdate()
    {
     
    }


    void FlipSprite()
    {
        if (transform.position.x<xCurrentPostion)
        {
            transform.localScale = new Vector2(-.4f, .4f);
           
        }

        else if(transform.position.x>xCurrentPostion)
        {
            transform.localScale = new Vector2(.4f, .4f);
            
        }
    }
}
