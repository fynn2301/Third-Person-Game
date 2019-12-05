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

enum AnimStateLeanAng
{
    LeanUp,
    LeanDown,
    Empty,
}

public class AnimatePlayer : MonoBehaviour
{
    // anim speed
    public float walkSpeedForeBack = 2.5f;
    public float walkSpeedDiagonal = 2.3f;
    public float walkSpeedLeftRight = 3f;
    float transformTimeBetweenAnimations = 0.3f;

    // camera container
    public GameObject cameraParent;

    // character/gun animator
    public Animator charAnimator;
    public Animator gunAnimator;
    public Animation[] characterAnimations;

    // joystick object
    public GameObject joystick;

    // joystick script
    WalkJoystick walkJoystick;

    // animation state
    AnimState animState = AnimState.Standing;
    AnimStateLeanAng animStateLeanAng = AnimStateLeanAng.Empty;

    // Start is called before the first frame update
    void Start()
    {
        walkJoystick = joystick.GetComponent<WalkJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
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
            /*
            if (movementVec.y > 0)
            {
                if (baseState != BaseState.Forwards)
                {
                    charAnimator.CrossFade("BodyRunningForwardsWithGunLow", transformTimeBetweenAnimations, 0, animTime);
                    charAnimator.CrossFade("BodyRunningForwardsWithGunUp", transformTimeBetweenAnimations, 2, animTime);
                    baseState = BaseState.Forwards;

                }

                if (movementVec.y > 0 && movementVec.x > 0)
                {
                    if (offsetState != RLOffsetState.Right)
                    {
                        charAnimator.CrossFade("OffsetLowerBodyRightRunning", transformTimeBetweenAnimations, 1, animTime);
                        charAnimator.CrossFade("OffsetUpperBodyRightRunningWithGun", transformTimeBetweenAnimations, 3, animTime);
                        offsetState = RLOffsetState.Right;
                    }
                    float offsetFactor = walkingAng / (Mathf.PI / 2);
                    charAnimator.SetLayerWeight(1, offsetFactor);
                    charAnimator.SetLayerWeight(3, offsetFactor);
                }
                else if (movementVec.y > 0 && movementVec.x < 0)
                {
                    if (offsetState != RLOffsetState.Left)
                    {
                        offsetState = RLOffsetState.Left;
                        charAnimator.CrossFade("OffsetLowerBodyLeftRunning", transformTimeBetweenAnimations, 1, animTime);
                        charAnimator.CrossFade("OffsetUpperBodyLeftRunningWithGun", transformTimeBetweenAnimations, 3, animTime);
                    }
                    float offsetFactor = walkingAng / (Mathf.PI / 4);
                    charAnimator.SetLayerWeight(1, offsetFactor);
                    charAnimator.SetLayerWeight(3, offsetFactor);
                }
            }
            */
            if (animState != AnimState.Forwards && (Mathf.Abs(walkingAng) <= Mathf.PI / 8) && movementVec.y > 0 && Mathf.Abs(movementVec.y) > Mathf.Abs(movementVec.x))
            {
                animState = AnimState.Forwards;
                controllAnimationControllers(animTime, "BodyRunningForwardsWithGun", "BodyRunningForwardsWithGun", "GunRunningForwards");

                charAnimator.speed = walkSpeedForeBack;
                gunAnimator.speed = walkSpeedForeBack;
            }
            // Forward-Right
            else if (animState != AnimState.ForwardsRight && walkingAng > Mathf.PI / 8 && walkingAng < Mathf.PI / 3 && movementVec.y > 0 && movementVec.x > 0)
            {
                animState = AnimState.ForwardsRight;
                controllAnimationControllers(animTime, "BodyRunningForwardRightWithGun", "BodyRunningForwardRightWithGun", "GunRunningForwardsRight");

                charAnimator.speed = walkSpeedDiagonal;
                gunAnimator.speed = walkSpeedDiagonal;
            }
            // Right
            else if (animState != AnimState.Right && (Mathf.Abs(walkingAng) >= Mathf.PI / 3) && movementVec.x > 0 && Mathf.Abs(movementVec.y) < Mathf.Abs(movementVec.x))
            {
                animState = AnimState.Right;
                controllAnimationControllers(animTime, "BodyRunningRightWithGun", "BodyRunningRightWithGun", "GunRunningRight");

                charAnimator.speed = walkSpeedLeftRight;
                gunAnimator.speed = walkSpeedLeftRight;
            }
            // Backward-Right
            else if (animState != AnimState.BackwardsRight && walkingAng < -Mathf.PI / 8 && walkingAng > -Mathf.PI / 3 && movementVec.y < 0 && movementVec.x > 0)
            {
                animState = AnimState.BackwardsRight;
                controllAnimationControllers(animTime, "BodyRunningBackwardsRightWithGun", "BodyRunningBackwardsRightWithGun", "GunRunningBackwardsRight");

                charAnimator.speed = walkSpeedDiagonal;
                gunAnimator.speed = walkSpeedDiagonal;
            }
            // Backward
            else if (animState != AnimState.Backwards && (Mathf.Abs(walkingAng) <= Mathf.PI / 8) && movementVec.y < 0 && Mathf.Abs(movementVec.y) > Mathf.Abs(movementVec.x))
            {
                animState = AnimState.Backwards;
                controllAnimationControllers(animTime, "BodyRunningBackwardsWithGun", "BodyRunningBackwardsWithGun", "GunRunningBackwards");

                charAnimator.speed = walkSpeedForeBack;
                gunAnimator.speed = walkSpeedForeBack;
            }
            // Backward-Left
            else if (animState != AnimState.BackwardsLeft && walkingAng > Mathf.PI / 8 && walkingAng < Mathf.PI / 3 && movementVec.y < 0 && movementVec.x < 0)
            {
                animState = AnimState.BackwardsLeft;
                controllAnimationControllers(animTime, "BodyRunningBackwardsLeftWithGun", "BodyRunningBackwardsLeftWithGun", "GunRunningBackwardsLeft");

                charAnimator.speed = walkSpeedDiagonal;
                gunAnimator.speed = walkSpeedDiagonal;
            }
            // Left
            else if (animState != AnimState.Left && (Mathf.Abs(walkingAng) >= Mathf.PI / 3) && movementVec.x < 0 && Mathf.Abs(movementVec.y) < Mathf.Abs(movementVec.x))
            {
                animState = AnimState.Left;
                controllAnimationControllers(animTime, "BodyRunningLeftWithGun", "BodyRunningLeftWithGun", "GunRunningLeft");

                charAnimator.speed = walkSpeedLeftRight;
                gunAnimator.speed = walkSpeedLeftRight;
            }
            // Forward-Left
            else if (animState != AnimState.ForwardsLeft && walkingAng < -Mathf.PI / 8 && walkingAng > -Mathf.PI / 3 && movementVec.y > 0 && movementVec.x < 0)
            {
                animState = AnimState.ForwardsLeft;
                controllAnimationControllers(animTime, "BodyRunningForwardLeftWithGun", "BodyRunningForwardLeftWithGun", "GunRunningForwardsLeft");

                charAnimator.speed = walkSpeedDiagonal;
                gunAnimator.speed = walkSpeedDiagonal;
            }
        }
        else if (animState != AnimState.Standing)
        {
            animState = AnimState.Standing;
            controllAnimationControllers(animTime, "BodyStandingWithGun", "BodyStandingWithGun", "GunStanding");

            charAnimator.CrossFade("BodyStandingWithGun", 0.1f, 0, animTime);
            charAnimator.CrossFade("BodyStandingWithGun", 0.1f, 1, animTime);
            gunAnimator.CrossFade("GunStanding", 0.1f, 0, animTime);

            charAnimator.speed = 1f;
            gunAnimator.speed = 1f;
        }
        float leanAng = cameraParent.transform.localEulerAngles.x;
        if (leanAng > 180f)
        {
            leanAng -= 360f;
        }
        // set ang of up down lean
        if (leanAng < 0 && animStateLeanAng != AnimStateLeanAng.LeanUp)
        {
            charAnimator.CrossFade("BodyLeanUpWithGun", 0.1f, 2, animTime);
            animStateLeanAng = AnimStateLeanAng.LeanUp;
        }
        else if (leanAng > 0 && animStateLeanAng != AnimStateLeanAng.LeanDown)
        {
            charAnimator.CrossFade("BodyLeanDownWithGun", 0.1f, 2, animTime);
            animStateLeanAng = AnimStateLeanAng.LeanDown;
        }
        else if (leanAng == 0 && animStateLeanAng != AnimStateLeanAng.Empty)
        {
            charAnimator.CrossFade("Empty", 0.1f, 1, animTime);
            animStateLeanAng = AnimStateLeanAng.Empty;
        }


        float leanFactor = Mathf.Abs(leanAng / 30f);
        if (leanFactor > 1)
        {
            leanFactor = 1;
        }
        else if (leanFactor < 0)
        {
            leanFactor = 0;
        }
        charAnimator.SetLayerWeight(2, leanFactor);

        //Debug.Log(leanFactor.ToString());
        // set animator variable

    }

    void controllAnimationControllers(float animTime, string stateNameLowerBody, string stateNameUpperBody, string stateNameGun = null)
    {
        charAnimator.CrossFade(stateNameLowerBody, transformTimeBetweenAnimations, 0, animTime);
        charAnimator.CrossFade(stateNameUpperBody, transformTimeBetweenAnimations, 1, animTime);
        gunAnimator.CrossFade(stateNameGun, transformTimeBetweenAnimations, 0, animTime);
    }

    public void shootAnimationGun(float fireRate)
    {
        float shotSpeed = 1 / fireRate;
        charAnimator.SetFloat("ShotSpeed", shotSpeed);
        charAnimator.SetTrigger("Shot");
    }
}
