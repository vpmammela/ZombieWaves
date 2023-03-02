using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;
    public LayerMask whatIsGround;
    bool isGrounded;
    bool readyToJump = true;

    [SerializeField]
    float movementSpeed = 5f;

    [SerializeField]
    float playerHeight;

    [SerializeField]
    float groundDrag;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float jumpCoolDown;

    [SerializeField]
    float airMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (isGrounded)
        {
            rb.drag = groundDrag;
            rb.AddRelativeForce(Vector3.right * horizontalInput * movementSpeed);
            rb.AddRelativeForce(Vector3.forward * verticalInput * movementSpeed);
        }
        else if (!isGrounded)
        {
            rb.drag = 0;
            rb.AddRelativeForce(Vector3.right * horizontalInput * movementSpeed * airMultiplier);
            rb.AddRelativeForce(Vector3.forward * verticalInput * movementSpeed * airMultiplier);
        }


        SpeedControl();

        // Ground check

        if(Input.GetKeyDown("space") && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
