using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Script : MonoBehaviour
{

    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;
    private int _lives = 2;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        player = GameObject.Find("Player").transform;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position - new Vector3(.2f, .2f, .2f);
            Debug.Log("Player detected!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position - new Vector3(.2f, .2f, .2f);
            Debug.Log("Attacking!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = locations[locationIndex].position;

            Debug.Log("Player Gone");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Slash(Clone)")
        { 
            _lives -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_lives <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Enemy down.");
        }
    }
}
