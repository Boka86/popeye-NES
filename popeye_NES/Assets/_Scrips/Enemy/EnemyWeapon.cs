using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speed;
    [SerializeField] private float destroyTimer;
    [SerializeField] Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Destroy(gameObject, destroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowrdPlayer();
    }

    void MoveTowrdPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("i Hit : " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
