using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBat : MonoBehaviour
{ 
    public float levelSpeed;
    public float lifeTimer;

    private float sidetosideSpeed;
    private float minSpeed = 1f;
    private float maxSpeed = 2f;

    private bool movingRight;
    private Vector3 initialPosition;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        audioManager.PlaySFX(audioManager.walkBat);

        // Randomly set the initial direction
        movingRight = Random.Range(0, 2) == 0;

        // Store the initial position of the enemy
        initialPosition = transform.position;

        // Randomize the speed between the specified values
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        sidetosideSpeed = randomSpeed;
    }

    void Update()
    {
        moveSelf();

        // Move the enemy horizontally
        MoveHorizontal();

        // Check if the enemy has reached the allowed range and needs to turn around
        CheckForTurn();
    }

    private void moveSelf()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
        }

        transform.position = transform.position + Vector3.left * Time.deltaTime * levelSpeed;
    }

    void MoveHorizontal()
    {
        // Calculate the movement vector
        Vector3 movement = new Vector3((movingRight ? 1 : -1) * sidetosideSpeed * Time.deltaTime, 0f, 0f);

        // Apply the movement to the enemy
        transform.Translate(movement);
    }

    void CheckForTurn()
    {
        // Check if the enemy has reached the right or left end of the allowed range
        if (transform.position.x >= initialPosition.x + 2f)
        {
            // If at the right end, start moving left
            movingRight = false;
        }
        else if (transform.position.x <= initialPosition.x - 2f)
        {
            // If at the left end, start moving right
            movingRight = true;
        }
    }
}
