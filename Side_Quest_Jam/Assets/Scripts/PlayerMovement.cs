using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    
        [SerializeField] private float speed = 5f; // Velocidade de movimento do objeto
        [SerializeField] private float attackDist;
        [SerializeField] private float attackAngle;

        [SerializeField] private GameObject[] Inimigo;

        private Rigidbody2D rb;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject enemy;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

        }

        private void FixedUpdate()
        {
            Inimigo = GameObject.FindGameObjectsWithTag("Inimigo");
            Attack();

            // Movimento usando as teclas W, A, S e D
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.velocity = movement * speed * Time.deltaTime;

            // Olhar na dire��o do mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;
            transform.up = direction;
        }

        public void Death()
        {
            gameObject.transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        }
        public void Attack()
        {
        
            float distance;
            distance = Vector3.Distance(gameObject.transform.position, EnemyCloser().transform.position);

            if(distance < 3 && Input.GetButtonUp("Fire1"))
            {
                Destroy(EnemyCloser());
            }

            
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
