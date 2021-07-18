using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChase : MonoBehaviour
{
    public enum AIState{patrol = 0, chase = 1, attack = 2 };

    public float attackDistance = 2f;

    private NavMeshAgent agent;
    private Transform player;

    public AIState currentState
    {
        get { return _currentState; }
        set
        {
            StopAllCoroutines();
            _currentState = value;

            switch (currentState)
            {
                case AIState.patrol:
                    StartCoroutine(StatePatrol());
                    break;

                case AIState.chase:
                    StartCoroutine(StateChase());
                    break;

                case AIState.attack:
                    StartCoroutine(StateAttack());
                    break;
            }
        }
    }

    [SerializeField]
    private AIState _currentState = AIState.patrol;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        currentState = AIState.patrol;
    }

/*    public void ChangeState(AIState newState)
    {
        StopAllCoroutines();
        currentState = newState;

        switch (currentState)
        {
            case AIState.patrol:
                StartCoroutine(StatePatrol());
                break;

            case AIState.chase:
                StartCoroutine(StateChase());
                break;

            case AIState.attack:
                StartCoroutine(StateAttack());
                break;
        }
    }*/

    public IEnumerator StateChase()
    {
        
        while(currentState == AIState.chase)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < attackDistance)
            {
                currentState = AIState.attack;
                yield break;
            }
            agent.SetDestination(player.transform.position);
            yield return null;
        }
        
    }

    public IEnumerator StateAttack()
    {
        while(currentState == AIState.attack)
        {
            if(Vector3.Distance(transform.position, player.transform.position) > attackDistance)
            {
                currentState = AIState.chase;
                yield break;
            }

            print("Attack!");
            agent.SetDestination(player.transform.position);
            yield return null;
        }
    }
/*
    private void Update()
    {
        agent.SetDestination(player.position);
    }*/

    public IEnumerator StatePatrol()
    {
        GameObject[] wayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
        GameObject currWayPoint = wayPoints[Random.Range(0, wayPoints.Length)];
        float targetDistance = 2f;

        while(currentState == AIState.patrol)
        {
            agent.SetDestination(currWayPoint.transform.position);

            if(Vector3.Distance(transform.position, currWayPoint.transform.position) < targetDistance)
            {
                currWayPoint = wayPoints[Random.Range(0, wayPoints.Length)];
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        currentState = AIState.chase;
    }

}
