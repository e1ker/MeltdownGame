using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirectionScript : MonoBehaviour
{
    public Transform playerOrientation;
    public Transform player;

    

    public Rigidbody rb;
    public float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        playerOrientation.forward = viewDirection.normalized;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector3 inputDirection = playerOrientation.forward * vertInput + playerOrientation.forward * horInput;

        if (inputDirection != Vector3.zero)
            player.forward = Vector3.Slerp(player.forward, inputDirection.normalized, Time.deltaTime * rotSpeed);
    }
}
