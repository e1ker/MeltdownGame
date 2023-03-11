using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float movementSpeed;
    public Transform playerOrientation;
    float horInput;
    float verInput;
    Vector3 moveDirection;
    public KeyCode jumpKey;
    public float jumpForce;
    public float jumpingMoveLimiter;
    public LayerMask platform;
    bool onPlatform;
    Quaternion rbStanding;



    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbStanding = rb.rotation;
    }

    void PossibleInputs()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");

        if (onPlatform)
            rb.drag = 5;
        else
            rb.drag = 0;
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
        StandUp();
    }
    // Update is called once per frame
    void Update()
    {
        PossibleInputs();
        onPlatform = Physics.Raycast(transform.position, Vector3.down, 2 * 0.5f + 0.2f, platform);
        SpeedLimiter();
    }

    void MovePlayer()
    {
        moveDirection = playerOrientation.forward * verInput + playerOrientation.right * horInput;

        if(onPlatform)
        rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f * jumpingMoveLimiter, ForceMode.Force);

        if (Input.GetKey(jumpKey) && onPlatform)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void SpeedLimiter()
    {
        Vector3 currentVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if(currentVel.magnitude > movementSpeed)
        {
            Vector3 maxVel = currentVel.normalized * movementSpeed;
            rb.velocity = new Vector3(maxVel.x, rb.velocity.y, maxVel.z);
        }
    }

    void StandUp()
    {
        if (onPlatform)
            rb.rotation = Quaternion.Slerp(rb.rotation, rbStanding, 1);
    }
}
