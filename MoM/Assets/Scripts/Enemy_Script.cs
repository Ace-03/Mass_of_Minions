using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Script : MonoBehaviour
{
    public Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void MoveToNextPatrolLocation()
    {
        agent.destination = locations[locationIndex].position;
    }

    void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
