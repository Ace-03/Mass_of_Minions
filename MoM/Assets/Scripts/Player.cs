using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10;
    public float rotateSpeed = 75;
    public GameObject slash;
    public GameObject camera;
    public float slashDelay;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private Game_Manager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
        camera = GameObject.Find("Main Camera");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy" || collision.gameObject.name == "Missile(Clone)")
        {
            Debug.Log("Damage");
            _gameManager.HP -= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (Input.GetMouseButtonDown(0) && slashDelay <= 0)
        {
            GameObject newSlash = Instantiate(slash, this.transform.position + this.transform.rotation * new Vector3(0, 0, .7f), this.transform.rotation * Quaternion.Euler(0, 0, 45)) as GameObject;
            slashDelay = 1;
        }
        else
        {
            slashDelay -= Time.deltaTime;
        }
        /*
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }
    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }
}


