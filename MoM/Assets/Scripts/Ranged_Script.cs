using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ranged_Script : MonoBehaviour
{

    public Transform target;
    public Transform patrolRoute;
    public List<Transform> locations;
    public GameObject missile;
    public float missileSpeed = 45f;

    private float missileDelay;
    private int locationIndex = 0;
    private NavMeshAgent agent;
    private int _lives = 10;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();

        if (gameObject.tag == "Red_Minion")
        {
            target = GameObject.FindGameObjectWithTag("Blue_Minion").transform;
        }
        else if (gameObject.tag == "Blue_Minion")
        {
            target = GameObject.FindGameObjectWithTag("Red_Minion").transform;
        }
        

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
        if (gameObject.tag == "Red_Minion")
        {
            if (other.tag == "Blue_Tower")
            {
                Debug.Log("Attaking Blue Tower!");
                target = GameObject.FindGameObjectWithTag("Blue_Tower").transform;
            }
            else if (other.tag == "Blue_Minion")
            {
                Debug.Log("Attaking Blue Minion!");
                target = GameObject.FindGameObjectWithTag("Blue_Minion").transform;
            }
        }

        if (gameObject.tag == "Blue_Minion")
        {
            if (other.tag == "Red_Tower")
            {
                Debug.Log("Attaking Red Tower!");
                target = GameObject.FindGameObjectWithTag("Red_Tower").transform;
            }
            else if (other.tag == "Red_Minion")
            {
                Debug.Log("Attaking Red Minion!");
                target = GameObject.FindGameObjectWithTag("Red_Minion").transform;
            }
        }

        if (other.transform == target)
        {
            agent.destination = target.position - new Vector3(2f, 2f, 2f);
            //Debug.Log(player.position);
            Debug.Log("Player detected!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        


        if (other.transform == target)
        {
            agent.destination = target.position - new Vector3(2f, 2f, 2f);
            Debug.Log("Attacking!");

            if (missileDelay >= 0)
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
        if (other.transform == target)
        {
            agent.destination = locations[locationIndex].position;
            Debug.Log("Player Gone");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Red_Minion")
        {
            if (collision.gameObject.tag == "Blue_Weapon")
            {
                Debug.Log("I took damage");
                _lives -= 1;
            }
        }

        if (gameObject.tag == "Blue_Minion")
        {
            if (collision.gameObject.tag == "Red_Weapon")
            {
                Debug.Log("I took damage");
                _lives -= 1;
            }
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