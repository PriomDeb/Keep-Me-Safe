using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private string isRunning = "isRunning";
    [SerializeField] private float adjustViewPortTransition = 0.2f;

    private Rigidbody rb;
    private Camera mainCamera;

    private Animator animator;

    private Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        animator = GetComponent<Animator>();
    }

    void Update()
    {

        try
        {
            if (Mouse.current.IsPressed(1f))
            {
                MouseControlledMovement();
            }
        }
        catch (InvalidOperationException)
        {
            Debug.Log("Error Passed");
        }

        /*ProcessInput();*/

        KeepPlayerOnScreen();

        RotateToFaceVelocity();
    }

    // 2. Updating Movement.
    void FixedUpdate()
    {
        if(movementDirection == Vector3.zero) { return; }

        rb.AddForce(movementDirection * forceMagnitude * Time.deltaTime, ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    // 1. Processing Touch Input
    void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {

            animator.SetBool(isRunning, true);

            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            // Getting a Vector3 from PlayerObject to TouchPoint
            movementDirection = transform.position - worldPosition;
            movementDirection.z = 0;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    // 2. Processing Rotation
    private void RotateToFaceVelocity()
    {
        if(rb.velocity == Vector3.zero) { return; }

        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        /*Debug.Log(viewportPosition);*/

        if (viewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + adjustViewPortTransition;
        }

        else if (viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - adjustViewPortTransition;
        }

        else if (viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + adjustViewPortTransition;
        }

        else if (viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - adjustViewPortTransition;
        }

        transform.position = newPosition;
    }


    private void MouseControlledMovement()
    {
        animator.SetBool(isRunning, true);

        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.farClipPlane * 0.5f;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        movementDirection = transform.position - worldPoint;
        movementDirection.z = 0;
        movementDirection.Normalize();
    }


}
