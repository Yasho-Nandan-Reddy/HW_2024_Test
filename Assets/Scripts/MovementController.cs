using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementspeed = 5.0f;
    public float rotationspeed = 2.0f;
    [SerializeField] private GameObject pulpit;
    private GameObject pulpitinstance;
    private GameObject newpulpitinstance;
    public float pulpittime = 5.0f;
    private float timeTracker = 0.0f;
    private bool pulpitDestroyed = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 spawnpoint = new Vector3(0, 0, 0);
        pulpitinstance = Instantiate(pulpit, spawnpoint, Quaternion.identity);
    }

    void newPulpit()
    {
        Vector3[] possibleOffsets = {
            new Vector3(9, 0, 0),
            new Vector3(-9, 0, 0),
            new Vector3(0, 0, 9),
            new Vector3(0, 0, -9)
        };

        Vector3 currentPulpitPosition = pulpitinstance.transform.position;
        Vector3 selectedOffset = possibleOffsets[Random.Range(0, possibleOffsets.Length)];
        Vector3 newPulpitPosition = currentPulpitPosition + selectedOffset;

        newpulpitinstance = Instantiate(pulpit, newPulpitPosition, Quaternion.identity);
        pulpitDestroyed = false; // Reset the destruction flag
    }

    void HandleMovement()
    {
        float forward = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(0, 0, forward) * movementspeed * (Time.deltaTime * 2);
        rb.MovePosition(transform.position + transform.TransformDirection(movement));

        Quaternion turn = Quaternion.Euler(0, rotation * rotationspeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * turn);
    }

    void Destroypulpit()
    {
        Destroy(pulpitinstance);
        pulpitinstance = newpulpitinstance;
        pulpitDestroyed = false; // Reset the flag so that a new pulpit can be generated in the next cycle
    }

    void Update()
    {
        HandleMovement();

        timeTracker += Time.deltaTime;

        if (timeTracker >= pulpittime / 2 && pulpitinstance != null && !pulpitDestroyed)
        {
            newPulpit();
            pulpitDestroyed = true;
        }

        if (timeTracker >= pulpittime)
        {
            Destroypulpit();
            timeTracker = 2.5f; // Reset the timer for the next cycle
        }
    }
}
