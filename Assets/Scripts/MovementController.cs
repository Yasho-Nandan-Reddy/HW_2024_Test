using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // New Variables for Death Mechanic and Scoring System
    public float fallThreshold = -5.0f;
    private bool isDead = false;
    private int score = 0; // Tracks the number of pulpits crossed

    public Text ScoreText; // Reference to the score display

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 spawnpoint = new Vector3(0, 0, 0);
        pulpitinstance = Instantiate(pulpit, spawnpoint, Quaternion.identity);
    }

    void newPulpit()
    {
        Vector3[] possibleOffsets = {
            new Vector3(8, 0, 0),
            new Vector3(-8, 0, 0),
            new Vector3(0, 0, 8),
            new Vector3(0, 0, -8)
        };

        Vector3 currentPulpitPosition = pulpitinstance.transform.position;
        Vector3 selectedOffset = possibleOffsets[Random.Range(0, possibleOffsets.Length)];
        Vector3 newPulpitPosition = currentPulpitPosition + selectedOffset;
        Vector3 oldPulpitPosition = pulpitinstance.transform.position;
        // Define boundaries
        float minX = -24f;
        float maxX = 24f;
        float minZ = -24f;
        float maxZ = 24f;


        // Ensure the new position is different from the old one and within bounds
        while (newPulpitPosition == oldPulpitPosition ||
               newPulpitPosition.x < minX || newPulpitPosition.x > maxX ||
               newPulpitPosition.z < minZ || newPulpitPosition.z > maxZ)
        {
            selectedOffset = possibleOffsets[Random.Range(0, possibleOffsets.Length)];
            newPulpitPosition = currentPulpitPosition + selectedOffset;
        }

        newpulpitinstance = Instantiate(pulpit, newPulpitPosition, Quaternion.identity);
        pulpitDestroyed = false;

        // Increment score when a new pulpit is generated and the player hasn't died
        if (!isDead)
        {
            score++;
            Debug.Log("Score: " + score);
            ScoreText.text = score.ToString();
        }
    }

    bool IsValidPosition(Vector3 position)
    {
        return (position.x >= -27 && position.x <= 27 && position.z >= -27 && position.z <= 27);
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
        pulpitDestroyed = false;
    }

    void Die()
    {
        isDead = true;
        Debug.Log("The character has died.");
        Time.timeScale = 0;
    }

    void Update()
    {
        if (isDead) return;

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
            timeTracker = 2.5f;
        }

        if (transform.position.y < fallThreshold)
        {
            Die();
            Debug.Log("Score of Doofus: " + score);  
        }
    }
}
