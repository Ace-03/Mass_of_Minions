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
    public float missileDelay;
    public GameObject missile;
    public float missileSpeed = 45f;

    private int locationIndex = 0;
    private NavMeshAgent agent;
    private int _lives = 10;

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

            if (missileDelay > 0)
            {
                missileDelay -= Time.deltaTime;
            }
            else
            {
                GameObject newMissile = Instantiate(missile, this.transform.position + this.transform.rotation * new Vector3(1f, 1f, 1f), this.transform.rotation) as GameObject;
                missileDelay = 10;
            }
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
            Debug.Log("I took damage");
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