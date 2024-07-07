using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 20.0f;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float cameraDistance;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        cameraDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    void Update()
    {
        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Convert touch position to a point in the world
            Vector3 touchPosition = touch.position;
            touchPosition.z = cameraDistance;

            Vector3 worldTouchPoint = Camera.main.ScreenToWorldPoint(touchPosition);
            worldTouchPoint.y = transform.position.y; // Keep character movement on the same y-level
            moveDirection = (worldTouchPoint - transform.position).normalized;

            if (moveDirection != Vector3.zero)
            {
                MoveCharacter(moveDirection);
            }
        }
    }

    void MoveCharacter(Vector3 direction)
    {
        Vector3 movement = direction * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
}
