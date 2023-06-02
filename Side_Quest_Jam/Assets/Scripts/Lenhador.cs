using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Lenhador : MonoBehaviour
{

    public float raioVisao;

    private Animator animator;
    
    public NavMeshAgent agent;
    public Transform target;

    private Vector2 targetPosition;
    private Vector2 fowardNew;

    public LayerMask treeLayer;
    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.Play("Walk");
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = target.position;
        fowardNew = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
        transform.right = fowardNew;
        
        if (agent.remainingDistance < 2f)
        {
            Collider2D colisor = Physics2D.OverlapCircle(this.transform.position, this.raioVisao, treeLayer);
            if (colisor.CompareTag("Arvore"))
            {
                Destroy(gameObject);
            }
        }     
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioVisao);
}   }
