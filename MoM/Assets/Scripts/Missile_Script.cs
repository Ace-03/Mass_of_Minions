using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Missile_Script : MonoBehaviour
{
    private NavMeshAgent agent;
    
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (gameObject.tag == "Red_Weapon")
        {
            target = GameObject.FindGameObjectWithTag("Blue_Minion").transform;
        }
        else if (gameObject.tag == "Blue_Weapon")
        {
            target = GameObject.FindGameObjectWithTag("Red_Minion").transform;
        }

        agent.destination = target.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform == target || collision.gameObject.name == "Boundary")
        {
            Destroy(this.gameObject);
        }
    }
}
