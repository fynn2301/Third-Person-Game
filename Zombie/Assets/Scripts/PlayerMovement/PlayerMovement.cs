using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // <Settings>
    // walkspeed of player
    public float walkSpeed = 1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    // <Objects>
    // character controller of player
    public CharacterController controller;
    // joystick object
    public GameObject joystick;
    // gravity
    public LayerMask groundMask;
    public Transform groundCheck;
    // joystick script

    WalkJoystick walkJoystick;
    // gravity
    Vector3 velocity;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        walkJoystick = joystick.GetComponent<WalkJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // movmetn playr x-z
        float x = walkJoystick.InputVector.x * 0.1f;
        float z = walkJoystick.InputVector.y * 0.1f * walkSpeed;
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move);
        // implementing gravity
        velocity.y += gravity * Time.deltaTime;
        // check for grounding to reset falling speed
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
