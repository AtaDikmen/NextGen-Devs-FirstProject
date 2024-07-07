using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Joystick joystick;    

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement * moveSpeed;

        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }
}
