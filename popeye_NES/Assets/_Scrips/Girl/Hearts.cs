using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] bool canMoveDown;
    //----------------------------
    Animator anim;
    void Start()
    {
        canMoveDown = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();

    }

    void movement()
    {
        if(canMoveDown)
        {
            transform.Translate(Random.Range(-6f, 6f) * speed * Time.deltaTime, Random.Range(-1f, -5f) * speed * Time.deltaTime, 0);
        }
       
   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Continer":
                anim.SetTrigger("StopRotate");
                canMoveDown = false;
                Debug.Log("I hit " + collision.gameObject.name);
                break;
            case "Player":
                Destroy(gameObject);
                break;
            default:
                break;
        }

    }
}
