using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBase : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento do objeto

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Movimento usando as teclas W, A, S e D
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed * Time.deltaTime;

        // Olhar na direção do mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        transform.up = direction;
    }
}
