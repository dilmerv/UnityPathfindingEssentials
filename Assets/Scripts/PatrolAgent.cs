using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private float minRemainingDistance = 0.5f;

    private int destinationPoint = 0;

    private NavMeshAgent agent;


    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if(points.Length == 0)
        {
            Debug.LogError("You must setup at least one destination point");
            enabled = false;
            return;
        }

        agent.destination = points[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % points.Length;
    }

    private void Update() 
    {
        if(!agent.enabled)
        {
            return;
        }
        
        if(!agent.pathPending && agent.remainingDistance < minRemainingDistance)
        {
            Debug.Log(destinationPoint);
            GoToNextPoint();
        }
    }
}
