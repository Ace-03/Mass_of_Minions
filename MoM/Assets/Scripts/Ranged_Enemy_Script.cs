using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ranged_Enemy_Script : MonoBehaviour
{

    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;
    public float timeLeft;
    public GameObject missile;
    public float missileSpeed = 45f;

    private int locationIndex = 0;
    private NavMeshAgent agent;

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
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position - new Vector3(2f, 2f, 2f);
            //Debug.Log(player.position);
            Debug.Log("Player detected!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position - new Vector3(2f, 2f, 2f);
            Debug.Log("Attacking!");
        }

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            GameObject newMissile = Instantiate(missile, this.transform.position + this.transform.rotation * new Vector3(1f, 1f, 1f), this.transform.rotation) as GameObject;
            //Rigidbody missileRB = newMissile.GetComponent<Rigidbody>();
            timeLeft = 5;
            //missileRB.velocity = this.transform.forward * missileSpeed - this.transform.right * 2f;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}