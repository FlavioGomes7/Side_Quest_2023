using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Lenhador : MonoBehaviour
{
    
    public NavMeshAgent agent;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false; 
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
