using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //(Jow - adiçao)
    private GameManager gameManager;
    private SaveManager saveManager;
    //-
    [SerializeField] private float speed; // Velocidade de movimento do objeto
    [SerializeField] private GameObject enemyDestroy;
    [SerializeField] private GameObject[] Inimigo;
    private AudioSource playerAtackAudio;
    //(Jow - adiçao)
    private GameObject inimigoMorto;
    //-
    private Rigidbody2D rb;
    [SerializeField] private Transform spawnPoint;
    private Animator animator;
    private bool isStopped;
    //(Jow - adiçao)
    private bool inimigoMortoProcessado = false;
    //-
    private void Start()
    {
        playerAtackAudio = GetComponent<AudioSource>();
        saveManager = FindObjectOfType<SaveManager>().GetComponent<SaveManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        Inimigo = GameObject.FindGameObjectsWithTag("Inimigo");
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Inimigo = GameObject.FindGameObjectsWithTag("Inimigo");
        


        if(!isStopped && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            animator.SetBool("IsWalking",true);
            //SoundWalking(Jow)
            
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
        if( (other.gameObject.CompareTag("Lenhador") && Input.GetButton("Fire1")) || (other.gameObject.CompareTag("Inimigo") && Input.GetButton("Fire1") ))
        {
            animator.SetBool("IsAttacking", true);
            isStopped = true;
            StartCoroutine(AttackAnim());
            //(Jow - adiçao)
            inimigoMorto =other.gameObject;
            //-
            
        }
    }
    //chama funcao Contabil de InimigosMortos de GameManager (Jow -adição)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Lenhador") && isStopped))
        {
            if (!inimigoMortoProcessado)
            {
                playerAtackAudio.Play();
                inimigoMortoProcessado = true;
                gameManager.InimigosMorto();
            }
        }
        else if(collision.gameObject.CompareTag("Inimigo") && isStopped)
        {
            playerAtackAudio.Play();
        }
    }

    public void Death()
    {
        gameObject.transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
    }


    public IEnumerator AttackAnim()
    {
        //Destroy(EnemyCloser());
        Instantiate(enemyDestroy, inimigoMorto.transform.position, Quaternion.identity);
        Destroy(inimigoMorto);
        yield return new WaitForSecondsRealtime(0.6f);
        Destroy(inimigoMorto);
        animator.SetBool("IsAttacking", false);
        isStopped = false;
        //(Jow - adiçao)
        inimigoMortoProcessado = false;
        //-
        
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
