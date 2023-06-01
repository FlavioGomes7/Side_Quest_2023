using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Arvore : MonoBehaviour
{
    public float raioVisao;
    private GameManager gameManager;

    public Sprite deadTree;
    public Sprite spriteOrigin;


    public LayerMask treeLayer;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
       Collider2D colisor = Physics2D.OverlapCircle(this.transform.position, this.raioVisao, treeLayer);
       if (colisor != null && colisor.CompareTag("Lenhador") && colisor.gameObject.GetComponent<Lenhador>().target == gameObject.transform)
       {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = deadTree;
            gameManager.ArvoreDerrubada();
       }
        //else
        {
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteOrigin;
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioVisao);
    }


}


