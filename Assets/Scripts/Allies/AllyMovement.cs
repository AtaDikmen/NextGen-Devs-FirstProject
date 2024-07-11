using UnityEngine;
using UnityEngine.AI;

public class AllyMovement : Movement
{
    [SerializeField] private Transform target;
    [SerializeField] private float followRadius;
    [SerializeField] private float circleResetTime;

    private float currentResetTime = 0f;
    private Vector3 randomCircle;
    private Vector3 currentCircle;
    private Vector3 targetPos;
    private NavMeshAgent agent;
    void Awake()
    {
        character = GetComponent<Ally>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        CheckRandomCircle();
        currentCircle = randomCircle;
        agent.speed = character.GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorVariables();
        CheckRandomCircle();
        SetCurrentCircle();
        CheckIfStopped();
    }
    void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        animator.SetBool("isWalking", true);
        targetPos = target.position + currentCircle;
        agent.SetDestination(targetPos);
        moveDirection = agent.velocity.normalized;

        if (character.isAttacking)
        {
            agent.angularSpeed = 0;
        }
        else
        {
            agent.angularSpeed = 360;
        }
    }

    private void SetCurrentCircle()
    {
        currentCircle = Vector3.MoveTowards(currentCircle, randomCircle, Time.deltaTime * 3);
    }

    private void CheckRandomCircle()
    {
        if (currentResetTime <= 0f)
        {
            SetRandomCircle();
            currentResetTime = circleResetTime;
        }
        else
        {
            currentResetTime -= Time.deltaTime;
        }
    }
    private void SetRandomCircle()
    {
        Vector2 circle = UnityEngine.Random.insideUnitCircle.normalized * followRadius;
        randomCircle = new Vector3(circle.x, 0f, circle.y);
    }
    private void CheckIfStopped()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.velocity.sqrMagnitude == 0f)
        {
            // Agent has stopped moving
            animator.SetBool("isWalking", false);
        }
    }
    public override void StartRunning()
    {
        if (!isRunning)
        {
            base.StartRunning();
            agent.speed *= runMultiplier;
        }
    }
    public override void StopRunning()
    {
        if (isRunning)
        {
            base.StopRunning();
            agent.speed /= runMultiplier;
        }
    }
}
