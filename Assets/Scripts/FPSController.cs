using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
    public float jumpPower = 8.0f;
    public float gravity = 10.0f;

    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    Rigidbody rb;
    public ThrowableObject throwableComponent;
    public Swapper s; 
    public Animator handAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Ensures that the player's Rigidbody doesn't rotate unexpectedly

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (canMove)
        {
            // Handles Movement
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
            float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        
            Vector3 direction = forward * curSpeedX + right * curSpeedY;
            moveDirection = new Vector3(direction.x, rb.linearVelocity.y, direction.z);

            // Handles Jumping
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                moveDirection.y = jumpPower;
            }

            // Apply Gravity
            moveDirection.y -= gravity * Time.deltaTime;

            // Apply Movement
            rb.linearVelocity = moveDirection;
        }

        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        if (Input.GetMouseButtonDown(0)) // Assuming left mouse button for throwing
        {
            throwableComponent.Throw();
        }
        if (Input.GetMouseButtonDown(1))
        {
            s.Swap();
            handAnimator.SetTrigger("Snap");
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}