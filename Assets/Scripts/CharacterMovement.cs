using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    //Adds character movement to the assigned game object
    public Entity character;
    private const int rotationConstant = 100;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float cameraDistance;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Set the initial distance from the camera to the character
        cameraDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    void Update()
    {
        HandleTouchInput(); // Handle touch input every frame
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            MoveCharacter(moveDirection); // Apply movement and rotation every fixed update for smooth physics
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Convert touch position to a point in the world
            Vector3 touchPosition = touch.position;
            touchPosition.z = cameraDistance; // Maintain the distance from the camera

            Vector3 worldTouchPoint = Camera.main.ScreenToWorldPoint(touchPosition);
            worldTouchPoint.y = transform.position.y; // Keep character movement on the same y-level
            moveDirection = (worldTouchPoint - transform.position).normalized;

            if (touch.phase == TouchPhase.Ended)
            {
                moveDirection = Vector3.zero;
                cameraDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
            }
        }
    }

    void MoveCharacter(Vector3 direction)
    {
        Vector3 movement = direction * character.GetSpeed() * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Rotate the character to face the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationConstant * character.GetRotationSpeed() * Time.deltaTime));
    }
}
