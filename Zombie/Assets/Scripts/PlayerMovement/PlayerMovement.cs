using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // <Settings>
    // walkspeed of player
    public float walkSpeed = 1f;
    public float walkSpeedForeBack = 1f;
    public float walkSpeedDiagonal = 1f;
    public float walkSpeedLeftRight = 1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    // <Objects>
    // character/gun animator
    public Animator charAnimator;
    public Animator gunAnimator;
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
    // animation state
    int animState = 0;
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

        // Animation settings
        CheckAnimationState(move);
    }


    void CheckAnimationState(Vector3 movementVec)
    {
        
        // check movement vector to animation state
        if (movementVec.magnitude > 0)
        {
            // get angular to get walking direction
            float walkingAng = Mathf.Atan(movementVec.x / movementVec.z);
            Debug.Log(walkingAng.ToString());
            if (Mathf.Abs(movementVec.z) >= Mathf.Abs(movementVec.x))
            {
                // Forward
                if ((Mathf.Abs(walkingAng) <= Mathf.PI / 8) && movementVec.z > 0 && Mathf.Abs(movementVec.z) > Mathf.Abs(movementVec.x))
                {
                    animState = 1;
                    charAnimator.speed = walkSpeedForeBack;
                    gunAnimator.speed = walkSpeedForeBack;
                }
                // Forward-Right
                else if (walkingAng > Mathf.PI / 8 && walkingAng < Mathf.PI / 4 && movementVec.z > 0 && movementVec.x > 0)
                {
                    animState = 6;
                    charAnimator.speed = walkSpeedDiagonal;
                    gunAnimator.speed = walkSpeedDiagonal;
                }
                // Right
                else if ((Mathf.Abs(walkingAng) >= Mathf.PI / 4) && movementVec.x > 0 && Mathf.Abs(movementVec.z) < Mathf.Abs(movementVec.x))
                {
                    animState = 3;
                    charAnimator.speed = walkSpeedLeftRight;
                    gunAnimator.speed = walkSpeedLeftRight;
                }
                // Backward-Right
                else if (walkingAng < -Mathf.PI / 8 && walkingAng > -Mathf.PI / 4 && movementVec.z < 0 && movementVec.x > 0)
                {

                }
                // Backward
                else if ((Mathf.Abs(walkingAng) <= Mathf.PI / 8) && movementVec.z < 0 && Mathf.Abs(movementVec.z) > Mathf.Abs(movementVec.x))
                {
                    animState = 2;
                    charAnimator.speed = walkSpeedForeBack;
                    gunAnimator.speed = walkSpeedForeBack;
                }
                // Backward-Left
                else if (walkingAng > Mathf.PI / 8 && walkingAng < Mathf.PI / 4 && movementVec.z < 0 && movementVec.x < 0)
                {

                }
                // Left
                else if ((Mathf.Abs(walkingAng) >= Mathf.PI / 4) && movementVec.x < 0 && Mathf.Abs(movementVec.z) < Mathf.Abs(movementVec.x))
                {
                    animState = 4;
                    charAnimator.speed = walkSpeedLeftRight;
                    gunAnimator.speed = walkSpeedLeftRight;
                }
                // Forward-Left
                else if (walkingAng < -Mathf.PI / 8 && walkingAng > -Mathf.PI / 4 && movementVec.z > 0 && movementVec.x < 0)
                {
                    animState = 5;
                    charAnimator.speed = walkSpeedDiagonal;
                    gunAnimator.speed = walkSpeedDiagonal;
                }
                Debug.Log(animState.ToString());
        } else
        {
            animState = 0;
            charAnimator.speed = 1f;
            gunAnimator.speed = 1f;
        }

        // set animator variable

        charAnimator.SetInteger("AnimationState", animState);
        gunAnimator.SetInteger("RunningState", animState);
    }
}
