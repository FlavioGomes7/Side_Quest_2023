using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    
    [SerializeField] private float speed = 5f; // Velocidade de movimento do objeto

    [SerializeField] private GameObject[] Inimigo;

    [SerializeField]private Rigidbody2D rb;
    [SerializeField] private Transform spawnPoint;
    private Animator animator;
    private bool isStopped;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Inimigo = GameObject.FindGameObjectsWithTag("Inimigo");
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        Inimigo = GameObject.FindGameObjectsWithTag("Inimigo");
        


        if(!isStopped && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            animator.SetBool("IsWalking",true);
            // Movimento usando as teclas W, A, S e D
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.velocity = movement * speed * Time.deltaTime;

        }
        else
        {    
            animator.SetBool("IsWalking", false);
            
        }
        // Olhar na dire��o do mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        transform.up = direction;  
        
    
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Inimigo") && Input.GetButton("Fire1"))
        {
            animator.SetBool("IsAttacking", true);
            isStopped = true;
            StartCoroutine(AttackAnim());
        }
    }

    public void Death()
    {
        gameObject.transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
    }


    public IEnumerator AttackAnim()
    {
        Destroy(EnemyCloser());
        yield return new WaitForSecondsRealtime(0.6f);
        animator.SetBool("IsAttacking", false);
        isStopped = false;            
    }

        
    public GameObject EnemyCloser()
    {
    float minDistance = Mathf.Infinity;
    float distance;
    int indexOfCloserEnemy = 0;
    for (int i = 0; i < Inimigo.Length; i++)
    {
   
        distance = Vector3.Distance(gameObject.transform.position, Inimigo[i].transform.position);
        if (minDistance > distance)
         {
            minDistance = distance;
            indexOfCloserEnemy = i;

         }

        }
        return Inimigo[indexOfCloserEnemy];
    }

    
       
}
