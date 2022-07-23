using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private Animator anim;
    private Rigidbody rb;

    [SerializeField] private float speed = 25f;
    [SerializeField] private Vector3 velocity;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField] private bool isOnGround = true;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private Slider slider;

    [SerializeField] private float flyHeight;

    float counterIncrease = 0;
    float counterDecrease = 0;

    [SerializeField] private ParticleSystem engineLeft;
    private ParticleSystem.MainModule leftMain;
    [SerializeField] private ParticleSystem engineRight;
    private ParticleSystem.MainModule rightMain;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        leftMain = engineLeft.main;
        rightMain = engineRight.main;
    }

    void Update()
    {
        Move();

        // Regen mana when character on the ground
        if (isOnGround)
        {
            counterIncrease += Time.deltaTime;
            if(counterIncrease > 2)
            {
                slider.value += 1;
                counterIncrease = 0;
            }
            leftMain.startColor = new Color(1, 0.427281f, 0);
            rightMain.startColor = new Color(1, 0.427281f, 0);
        }
        if(!isOnGround)
        {
            counterDecrease += Time.deltaTime;
            if (counterDecrease > 1)
            {
                slider.value -= 1;
                counterDecrease = 0;
            }
            leftMain.startColor = new Color(0.04950152f, 0.8034409f, 0.9716981f);
            rightMain.startColor = new Color(0.04950152f, 0.8034409f, 0.9716981f);
        }
        if(slider.value == 0)
        {
            velocity.y -= 0.5f;
        }
    }

    private void Move()
    {
        isOnGround = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if(isOnGround){
            speed = 25f;
        }
        else{
            speed = 50f;
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && slider.value > 0)
        {
            Fly();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    private void Fly()
    {
        speed = 50f;
        velocity.y = Mathf.Sqrt(flyHeight * -2f * gravity);
    }
}
