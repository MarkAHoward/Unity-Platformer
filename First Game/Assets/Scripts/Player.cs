using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    bool jumpKeyWasPressed;
    bool shiftKeyWasPressed;
    float horizontalInput;
    Rigidbody rigidbodyComponent;
    int superJumpsRemaining;
    


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            jumpKeyWasPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        
    }

    void FixedUpdate()
    {
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length != 0)
        {
            float jumpPower = 5f;

            if (jumpKeyWasPressed)
            {
                rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                jumpKeyWasPressed = false;
            }
            
            if (shiftKeyWasPressed)
            {       
                if (superJumpsRemaining > 0)
                {
                    rigidbodyComponent.AddForce(Vector3.up * (jumpPower * 2), ForceMode.VelocityChange);
                    superJumpsRemaining--;
                }
                shiftKeyWasPressed = false;
            }


        }

        rigidbodyComponent.velocity = new Vector3(horizontalInput * 2, rigidbodyComponent.velocity.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

}
