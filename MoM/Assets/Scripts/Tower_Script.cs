using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower_Script : MonoBehaviour
{
    public Transform target;  
    public GameObject Tower_Missile;
    public float missileSpeed = 45f;

    private float missileDelay;
    private int _lives = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Red_Tower")
        {
            target = GameObject.FindGameObjectWithTag("Blue_Minion").transform;
        }
        else if (gameObject.tag == "Blue_Tower")
        {
            target = GameObject.FindGameObjectWithTag("Red_Minion").transform;
        }

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
            Debug.Log(missileDelay);

            if (missileDelay >= 0)
            {
                missileDelay = 10;
                missileDelay -= Time.deltaTime;
            }
            else
            {
                GameObject newTowerMissile = Instantiate(Tower_Missile, this.transform.position + this.transform.rotation * new Vector3(1f, 1f, 1f), this.transform.rotation) as GameObject;
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

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Red_Tower")
        {
            if (collision.gameObject.tag == "Blue_Weapon")
            {
                Debug.Log("Red Tower took damage");
                _lives -= 1;
            }
        }

        if (gameObject.tag == "Blue_Tower")
        {
            if (collision.gameObject.tag == "Red_Weapon")
            {
                Debug.Log("Blue Tower took damage");
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
            Debug.Log("Tower down.");

            if (gameObject.tag == "Red_Tower")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (gameObject.tag == "Blue_Tower")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }

            //Invoke("GameOver", 2);
        }
    }

    /*
    private void GameOver()
    {
        if (gameObject.tag == "Red_Tower")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (gameObject.tag == "Blue_Tower")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
    */
}
