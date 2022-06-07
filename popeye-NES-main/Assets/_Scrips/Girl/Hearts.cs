using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] bool canMoveDown;
     Player player;
    //----------------------------
    Animator anim;
    //---------------------------
    AudioSource source;
   [SerializeField] AudioClip alramSound;
    [SerializeField] AudioClip heartCollectSound;
    void Start()
    {
        source = GetComponent<AudioSource>();
        canMoveDown = true;
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
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
                if(source.isPlaying==false)
                {
                    source.PlayOneShot(alramSound);
                }
                anim.SetTrigger("StopRotate");
                canMoveDown = false;
                StartCoroutine(HeartInContinerTimer());
                break;
            case "Player":
                source.PlayOneShot(heartCollectSound,20);
                
                player.HeartCount();
                Destroy(gameObject,.1f);
                break;
            default:
                break;
        }

    }

    IEnumerator HeartInContinerTimer()
    {
        yield return new WaitForSeconds(8f);
        player.GameOver();
    }

   
}
