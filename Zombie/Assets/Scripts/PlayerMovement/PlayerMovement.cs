using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AnimState
{
    Standing,
    Forwards,
    Backwards,
    ForwardsLeft,
    ForwardsRight,
    BackwardsLeft,
    BackwardsRight,
    Left,
    Right,
};

public class PlayerMovement : MonoBehaviour
{
    // <Settings>
    // walkspeed of player
    public float walkSpeed = 1f;
    public float walkSpeedForeBack = 2.5f;
    public float walkSpeedDiagonal = 2.3f;
    public float walkSpeedLeftRight = 3f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float transformTimeBetweenAnimations = 0.3f;

    // <Objects>
    // character/gun animator
    public Animator charAnimator;
    public Animator gunAnimator;

    public Animation[] characterAnimations;
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
    AnimState animState = AnimState.Standing;
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
        CheckAnimationState(walkJoystick.InputVector);
    }


    void CheckAnimationState(Vector2 movementVec)
    {
        float animTime = charAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        while (animTime > 1)
        {
            animTime -= 1;

        }
        
        // check movement vector to animation state
        if (movementVec.magnitude > 0)
        {
            // get angular to get walking direction
            float walkingAng = Mathf.Atan(movementVec.x / movementVec.y);
            // Forward
            if (animState != AnimState.Forwards && (Mathf.Abs(walkingAng) <= Mathf.PI / 8) && movementVec.y > 0 && Mathf.Abs(movementVec.y) > Mathf.Abs(movementVec.x))
            {
                animState = AnimState.Forwards;
                charAnimator.CrossFade("BodyRunningForwardsWithGun", transformTimeBetweenAnimations, 0, animTime);
                gunAnimator.CrossFade("GunRunningForwards", transformTimeBetweenAnimations, 0, animTime);

                charAnimator.speed = walkSpeedForeBack;
                gunAnimator.speed = walkSpeedForeBack;
            }
            // Forward-Right
            else if (animState != AnimState.ForwardsRight && walkingAng > Mathf.PI / 8 && walkingAng < Mathf.PI / 3 && movementVec.y > 0 && movementVec.x > 0)
            {
                animState = AnimState.ForwardsRight;
                charAnimator.CrossFade("BodyRunningForwardRightWithGun", transformTimeBetweenAnimations,  0, animTime);
                gunAnimator.CrossFade("GunRunningForwardsRight", transformTimeBetweenAnimations, 0, animTime);

                charAnimator.speed = walkSpeedDiagonal;
                gunAnimator.speed = walkSpeedDiagonal;
            }
            // Right
            else if (animState != AnimState.Right && (Mathf.Abs(walkingAng) >= Mathf.PI / 3) && movementVec.x > 0 && Mathf.Abs(movementVec.y) < Mathf.Abs(movementVec.x))
            {
                animState = AnimState.Right;
                charAnimator.CrossFade("BodyRunningRightWithGun", transformTimeBetweenAnimations, 0, animTime);
                gunAnimator.CrossFade("GunRunningRight", transformTimeBetweenAnimations, 0, animTime);

                charAnimator.speed = walkSpeedLeftRight;
                gunAnimator.speed = walkSpeedLeftRight;
            }
            // Backward-Right
            else if (animState != AnimState.BackwardsRight && walkingAng < -Mathf.PI / 8 && walkingAng > -Mathf.PI / 3 && movementVec.y < 0 && movementVec.x > 0)
            {
                animState = AnimState.BackwardsRight;
                charAnimator.CrossFade("BodyRunningBackwardsRightWithGun", transformTimeBetweenAnimations, 0, animTime);
                gunAnimator.CrossFade("GunRunningBackwardsRight", transformTimeBetweenAnimations, 0, animTime);

                charAnimator.speed = walkSpeedDiagonal;
                gunAnimator.speed = walkSpeedDiagonal;
            }
            // Backward
            else if (animState != AnimState.Backwards && (Mathf.Abs(walkingAng) <= Mathf.PI / 8) && movementVec.y < 0 && Mathf.Abs(movementVec.y) > Mathf.Abs(movementVec.x))
            {
                animState = AnimState.Backwards;
                charAnimator.CrossFade("BodyRunningBackwardsWithGun", transformTimeBetweenAnimations, 0, animTime);
                gunAnimator.CrossFade("GunRunningBackwards", transformTimeBetweenAnimations, 0, animTime);

                charAnimator.speed = walkSpeedForeBack;
                gunAnimator.speed = walkSpeedForeBack;
            }
            // Backward-Left
            else if (animState != AnimState.BackwardsLeft && walkingAng > Mathf.PI / 8 && walkingAng < Mathf.PI / 3 && movementVec.y < 0 && movementVec.x < 0)
            {
                animState = AnimState.BackwardsLeft;
                charAnimator.CrossFade("BodyRunningBackwardsLeftWithGun", transformTimeBetweenAnimations, 0, animTime);
                gunAnimator.CrossFade("GunRunningBackwardsLeft", transformTimeBetweenAnimations, 0, animTime);

                charAnimator.speed = walkSpeedDiagonal;
                gunAnimator.speed = walkSpeedDiagonal;
            }
            // Left
            else if (animState != AnimState.Left && (Mathf.Abs(walkingAng) >= Mathf.PI / 3) && movementVec.x < 0 && Mathf.Abs(movementVec.y) < Mathf.Abs(movementVec.x))
            {
                animState = AnimState.Left;
                charAnimator.CrossFade("BodyRunningLeftWithGun", transformTimeBetweenAnimations, 0, animTime);
                gunAnimator.CrossFade("GunRunningLeft", transformTimeBetweenAnimations, 0, animTime);

                charAnimator.speed = walkSpeedLeftRight;
                gunAnimator.speed = walkSpeedLeftRight;
            }
            // Forward-Left
            else if (animState != AnimState.ForwardsLeft && walkingAng < -Mathf.PI / 8 && walkingAng > -Mathf.PI / 3 && movementVec.y > 0 && movementVec.x < 0)
            {
                animState = AnimState.ForwardsLeft;
                charAnimator.CrossFade("BodyRunningForwardLeftWithGun", 0.3f, 0, animTime);
                gunAnimator.CrossFade("GunRunningForwardsLeft", 0.3f, 0, animTime);

                charAnimator.speed = walkSpeedDiagonal;
                gunAnimator.speed = walkSpeedDiagonal;
            }
        }
        else if (animState != AnimState.Standing)
        {
            animState = AnimState.Standing;
            charAnimator.CrossFade("BodyStandingWithGun", 0.2f, 0, animTime);
            gunAnimator.CrossFade("GunStanding", 0.2f, 0, animTime);

            charAnimator.speed = 1f;
            gunAnimator.speed = 1f;
        }
        Debug.Log(animState.ToString());
        // set animator variable


    }
}
