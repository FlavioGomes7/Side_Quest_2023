using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Lenhador : MonoBehaviour
{

    public float raioVisao;
    
    public NavMeshAgent agent;
    public Transform target;

    public LayerMask treeLayer;
    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (agent.remainingDistance < 1.9f)
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
