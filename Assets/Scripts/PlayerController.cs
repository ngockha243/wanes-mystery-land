using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private Animator anim;
    private Rigidbody rb;

    [SerializeField] private float speed = 6f;
    [SerializeField] private Vector3 velocity;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private bool isOnGround = true;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float flyHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        isOnGround = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if(isOnGround)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                // Get horizontal and vertical
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                if (direction.magnitude >= 0.1f)
                {
                    anim.SetBool("IsMove", true);
                    float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                    // rotate character by camera
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    // move character
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);

                }

            }
            // Not move
            else
            {
                anim.SetBool("IsMove", false);
            }

            // Fly
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fly();
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
    
    private void Fly()
    {
        velocity.y = Mathf.Sqrt(flyHeight * -2f * gravity);
    }
}
