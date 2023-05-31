using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Segurança : MonoBehaviour
{
    private State state;
    public NavMeshAgent agent;
    public GameObject target;
    public Transform visor; // Referência ao objeto do visor

    [Header("Enemy Properties")]
    [SerializeField] private float _patrollingSpeed = 1f;
    [SerializeField] private float _followingSpeed = 3f;
    [SerializeField] private float rotationSpeed; // Velocidade de rotação no eixo Z

    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        this.state = State.Patroling;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Following:
                Follow(target);
                break;
            default:
                Patrol();
                break;
        }
    }

    private void Patrol()
    {
        this.agent.speed = _patrollingSpeed;

        if (!this.agent.pathPending && !this.agent.hasPath)
        {
            this.agent.SetDestination(Random.insideUnitCircle.normalized * 3);
        }

    }

    private void Follow(GameObject target)
    {
        this.agent.speed = _followingSpeed;
        this.agent.SetDestination(target.transform.position);

      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                this.state = State.Following;
                this.target = other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                this.state = State.Patroling;
            }
        }
    }

    enum State { Patroling, Following };

}
