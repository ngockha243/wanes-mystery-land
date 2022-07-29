using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    // Camera
    [SerializeField] private Transform cam;
    // Animation
    [SerializeField] private Animator anim;

    // Speed
    [SerializeField] private float speed = 25f;
    public Vector3 velocity;

    // Smooth Turn
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField] private bool isOnGround = true;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    

    [SerializeField] private float flyHeight;

    // Mana bar

    float counterIncrease = 0;
    float counterDecrease = 0;


    int powerPoint = 0;
    [SerializeField] private Image[] power;

    // Engine
    [SerializeField] private ParticleSystem engineLeft;
    private ParticleSystem.MainModule leftMain;
    [SerializeField] private ParticleSystem engineRight;
    private ParticleSystem.MainModule rightMain;


    void Start()
    {
        // Get CharacterController
        controller = GetComponent<CharacterController>();

        leftMain = engineLeft.main;
        rightMain = engineRight.main;

        DisplayPower();
    }

    void Update()
    {
        Move();

        // Regen power when character on the ground
        if (isOnGround)
        {
            anim.SetBool("IsFly", false);
            counterIncrease += Time.deltaTime;
            if(counterIncrease > 2)
            {
                powerPoint += 1;
                counterIncrease = 0;
                DisplayPower();
            }
            leftMain.startColor = new Color(1, 0.427281f, 0);
            rightMain.startColor = new Color(1, 0.427281f, 0);
        }
        // Decrease power when character fly
        if(!isOnGround)
        {
            anim.SetBool("IsFly", true);
            counterDecrease += Time.deltaTime;
            if (counterDecrease > 1)
            {
                powerPoint -= 1;
                counterDecrease = 0;
                DisplayPower();
            }
            leftMain.startColor = new Color(0.04950152f, 0.8034409f, 0.9716981f);
            rightMain.startColor = new Color(0.04950152f, 0.8034409f, 0.9716981f);
        }
        // When power = 0: pull character to ground
        if(powerPoint == 0)
        {
            velocity.y -= 0.5f;
        }
    }

    private void Move()
    {
        isOnGround = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        // Double speed when character fly
        if(isOnGround){
            speed = 25f;
        }
        else{
            speed = 50f;
        }
        // Move
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            // Get horizontal and vertical
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            // If move
            if (direction.magnitude >= 0.1f)
            {
                // Set animation
                anim.SetBool("IsMove", true);

                // rotate character by camera
                float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // move character
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);

            }

        }
        // Not move
        else
        {
            anim.SetBool("IsMove", false);
        }

        // Fly
        if (Input.GetKeyDown(KeyCode.Space) && powerPoint > 0)
        {
            Fly();
        }
        if(!Physics.CheckSphere(transform.position, flyHeight, groundMask) && powerPoint != 0)
        {
            velocity.y = 0;
        }

        // Pull character to ground
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    private void Fly()
    {
        speed = 50f;
        velocity.y = Mathf.Sqrt(flyHeight * -2f * gravity);
        
    }

    void DisplayPower(){
        for(int i = 0; i < 10; i++){
            if(i < powerPoint){
                power[i].enabled = true;
            }
            else{
                power[i].enabled = false;
            }
        }
    }
}
