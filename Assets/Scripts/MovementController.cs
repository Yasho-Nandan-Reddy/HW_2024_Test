using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementspeed = 5.0f;
    public float rotationspeed = 2.0f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void HandleMovement()
    {
        float forward= Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(0, 0, forward) * movementspeed * Time.deltaTime;
        rb.MovePosition(transform.position + transform.TransformDirection(movement));

        Quaternion turn = Quaternion.Euler(0, rotation * rotationspeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * turn);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
}
