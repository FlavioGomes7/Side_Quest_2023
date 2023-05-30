using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float timer;
    [SerializeField] private float timerMax;
    [SerializeField] private float force;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timerMax)
        {
            Destroy(gameObject);
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
            Destroy(gameObject);
            other.GetComponent<PlayerMovement>().Death();

        }

    }
    

}
