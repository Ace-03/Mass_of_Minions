using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Script : MonoBehaviour
{
    public Transform target;
    public float missileDelay;
    public GameObject Tower_Missile;
    public float missileSpeed = 45f;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Red_Weapon")
        {
            target = GameObject.FindGameObjectWithTag("Blue_Minion").transform;
        }
        else if (gameObject.tag == "Blue_Weapon")
        {
            target = GameObject.FindGameObjectWithTag("Red_Minion").transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            //Debug.Log(player.position);
            Debug.Log("Tower Active!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform == target)
        {
            Debug.Log("Tower Attacking!");

            if (missileDelay > 0)
            {
                missileDelay -= Time.deltaTime;
            }
            else
            {
                GameObject newMissile = Instantiate(Tower_Missile, this.transform.position + this.transform.rotation * new Vector3(1f, 1f, 1f), this.transform.rotation) as GameObject;
                missileDelay = 10;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
        {
            Debug.Log("Enemy Left Tower");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
