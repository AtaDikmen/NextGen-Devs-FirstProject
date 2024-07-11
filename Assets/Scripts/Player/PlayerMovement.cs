using UnityEngine;

public class PlayerMovement : Movement
{
    //Adds character movement to the assigned game object
    private Player player;
    private const int rotationConstant = 100;
    private Rigidbody rb;
    private float cameraDistance;

    void Awake()
    {
        character = GetComponent<Player>();
        player = (Player)character;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Set the initial distance from the camera to the character
        cameraDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    void Update()
    {
        SetAnimatorVariables();
        HandleTouchInput(); // Handle touch input every frame
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            MoveCharacter(moveDirection); // Apply movement and rotation every fixed update for smooth physics
        }
        else
        {
            animator.SetBool("isWalking", false);
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
        animator.SetBool("isWalking", true);

        Vector3 movement = direction * player.GetSpeed() * Time.deltaTime;
        rb.MovePosition(transform.position + movement);


        if (!player.isAttacking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            // Rotate the character to face the movement direction
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationConstant * player.GetRotationSpeed() * Time.deltaTime));
        }
    }

    public override void StartRunning()
    {
        if (!isRunning)
        {
            base.StartRunning();
            foreach (var ally in player.GetAllies())
            {
                ally.GetComponent<AllyMovement>().StartRunning();
            }
        }
    }
    public override void StopRunning()
    {
        if (isRunning)
        {
            base.StopRunning();
            foreach (var ally in player.GetAllies())
            {
                ally.GetComponent<AllyMovement>().StopRunning();
            }
        }
    }
}
