using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementspeed = 5.0f;
    public float rotationspeed = 2.0f;
    [SerializeField] private GameObject pulpit;
    private GameObject pulpitinstance;
    public float pulpittime = 5.0f;
    private GameObject[] pulpitarray;
    private int xory;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 spawnpoint= new Vector3(0, 0, 0);
        pulpitinstance = Instantiate(pulpit, spawnpoint, Quaternion.identity);
        Debug.Log(pulpitarray.Length);
        
    }

    void newPulpit()
    {
        xory = Random.Range(0, 2);
        Debug.Log(xory);
        if (xory == 0)
        {
            int x = Random.Range(-1, 2);
            if (x == 0 || x == 1) { x = 9;}
            else{x= -9;}
            Vector3 spawnpoint = new Vector3(x, 0, 0);
            GameObject newinstance = Instantiate(pulpit, spawnpoint, Quaternion.identity);
        }
        else
        {
            int x = Random.Range(-1, 2);
            if (x == 0||x==1){ x = 9; }
            else { x = -9; }
            Vector3 spawnpoint = new Vector3(0, 0, x);
            GameObject newinstance = Instantiate(pulpit, spawnpoint, Quaternion.identity);
        }
    }


    void HandleMovement()
    {
        float forward= Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(0, 0, forward) * movementspeed * (Time.deltaTime * 2);
        rb.MovePosition(transform.position + transform.TransformDirection(movement));

        Quaternion turn = Quaternion.Euler(0, rotation * rotationspeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * turn);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        float halfpulpittime = pulpittime / 2;
        Invoke("newPulpit", halfpulpittime);
        Destroy(pulpitinstance, pulpittime);
    }
}
