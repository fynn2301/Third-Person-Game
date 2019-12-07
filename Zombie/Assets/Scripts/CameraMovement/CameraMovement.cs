using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    // <settings>
    
    // turn speed
    public float mouseSensitivityX = 15;
    public float mouseSensitivityY = 10;

    // <Objects>S
    // playerbody to rotate aroud
    public Transform playerBody;

    // gameObject on player that rotats with the camera for y rotation
    public Transform yRotationCameraObject; 
    float xRotation = 0f;

    
    
    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    public int PointerId;
    [HideInInspector]
    public bool Pressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {
            // getting camera vec
            TouchDist = Input.touches[PointerId].position - PointerOld;
            TouchDist *= new Vector2(mouseSensitivityX * Time.deltaTime * 0.1f, 1);
            TouchDist *= new Vector2(1, mouseSensitivityY * Time.deltaTime * 0.1f);

            // moving camera x 
            playerBody.Rotate(Vector3.up * TouchDist.x);
            xRotation -= TouchDist.y;
            xRotation = Mathf.Clamp(xRotation, -30f, 30f);

            // and y
            yRotationCameraObject.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            PointerOld = Input.touches[PointerId].position;
        }


    }
}
