using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 20.0f;
    public float rotationSpeed = 20.0f;
    public int rotationConstant = 100;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float cameraDistance;
    public Button runButton;
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
        // Rotate the character to face the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationConstant * rotationSpeed * Time.deltaTime));
    }

    public void Run()
    {
        StartCoroutine(RunCoroutine());
    }

    private IEnumerator RunCoroutine()
    {
        speed *= 2;
        runButton.interactable = false;

        yield return new WaitForSeconds(5);

        speed /= 2;
        runButton.interactable = true;
    }
}
